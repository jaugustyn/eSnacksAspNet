using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eSnacks.Data.Services;
using eSnacks.Models;
using Microsoft.AspNetCore.Authorization;

namespace eSnacks.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CityController : Controller
    {
        private readonly ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }

        
        // GET: City
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cities = await _service.GetAllAsync();
            return cities != null ? View(cities) : Problem("Entity set 'ApplicationDbContext.City' is null.");
        }

        
        // GET: City/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return NotFound();
            return View(cityDetails);
        }

        
        // GET: City/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: City/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityName,ZipCode")]City city)
        {
            if (!ModelState.IsValid) return View(city);
            await _service.AddAsync(city);
            return RedirectToAction(nameof(Index));
        }
        

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return NotFound();
            return View(cityDetails);
        }
        
        // POST: City/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityName,ZipCode")] City city)
        {
            if (id != city.Id) return NotFound();
            
            if (!ModelState.IsValid) return View(city);
            
            try
            {
                await _service.UpdateAsync(id, city);
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

        
        // GET: City/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return NotFound();
            return View(cityDetails);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
