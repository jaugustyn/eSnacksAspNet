using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSnacks.Data.Services;
using eSnacks.Models;
using Microsoft.AspNetCore.Authorization;

namespace eSnacks.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        
        // GET: Category
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllAsync();
            return categories != null ? View(categories) : Problem("Entity set 'ApplicationDbContext.City' is null.");
        }

        
        // GET: Category/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return NotFound();
            return View(categoryDetails);
        }

        
        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName,Description")] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            await _service.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }
        

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return NotFound();
            return View(categoryDetails);
        }
        
        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Description")] Category category)
        {
            if (id != category.Id) return NotFound();

            if (!ModelState.IsValid) return View(category);
            
            try
            {
                await _service.UpdateAsync(id, category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        
        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return NotFound();
            return View(categoryDetails);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
