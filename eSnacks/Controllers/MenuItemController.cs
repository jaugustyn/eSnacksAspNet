using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eSnacks.Data.Services;
using eSnacks.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace eSnacks.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService _service;

        public MenuItemController(IMenuItemService service)
        {
            _service = service;
        }

        // GET: City
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var menuItems = await _service.GetAllAsync(mi => mi.Category, mi => mi.Restaurant);
            return menuItems != null ? View(menuItems) : Problem("Entity set 'ApplicationDbContext.City' is null.");
        }

        
        // GET: City/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var menuItemDetails = await _service.GetMenuItemByIdAsync(id);
            if (menuItemDetails == null) return NotFound();
            return View(menuItemDetails);
        }

        
        // GET: City/Create
        public async Task<IActionResult> Create()
        {
            var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();
            ViewBag.CategoryId = new SelectList(menuItemDropdownsData.Categories, "Id", "CategoryName");
            ViewBag.RestaurantId = new SelectList(menuItemDropdownsData.Restaurants, "Id", "RestaurantName");
            
            return View();
        }
        
        // POST: City/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemName,Description,Ingredients,Price,Available,PhotoUrl,CategoryId,RestaurantId")]NewMenuItemVM menuItem)
        {
            if (!ModelState.IsValid)
            {
                var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();
                ViewBag.CategoryId = new SelectList(menuItemDropdownsData.Categories, "Id", "CategoryName");
                ViewBag.RestaurantId = new SelectList(menuItemDropdownsData.Restaurants, "Id", "RestaurantName");
                
                return View(menuItem);
            }
            
            await _service.AddNewMenuItemAsync(menuItem);
            return RedirectToAction(nameof(Index));
        }
        

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var menuItemDetails = await _service.GetMenuItemByIdAsync(id);
            if (menuItemDetails == null) return NotFound();
            
            var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();
            ViewBag.CategoryId = new SelectList(menuItemDropdownsData.Categories, "Id", "CategoryName");
            ViewBag.RestaurantId = new SelectList(menuItemDropdownsData.Restaurants, "Id", "RestaurantName");

            var menuItemDetailsVM = new NewMenuItemVM()
            {
                Id = menuItemDetails.Id,
                ItemName = menuItemDetails.ItemName,
                Description = menuItemDetails.Description,
                PhotoUrl = menuItemDetails.PhotoUrl,
                Ingredients = menuItemDetails.Ingredients,
                Price = menuItemDetails.Price,
                Available = menuItemDetails.Available,
                CategoryId = menuItemDetails.CategoryId,
                RestaurantId = menuItemDetails.RestaurantId
            };
            
            return View(menuItemDetailsVM);
        }
        
        // POST: City/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,Description,Ingredients,Price,Available,PhotoUrl,CategoryId,RestaurantId")] NewMenuItemVM menuItem)
        {
            if (id != menuItem.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();
                ViewBag.CategoryId = new SelectList(menuItemDropdownsData.Categories, "Id", "CategoryName");
                ViewBag.RestaurantId = new SelectList(menuItemDropdownsData.Restaurants, "Id", "RestaurantName");
                
                return View(menuItem);
            }
            
            try
            {
                await _service.UpdateMenuItemAsync(menuItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetMenuItemByIdAsync(id) == null)
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
            var menuItemDetails = await _service.GetByIdAsync(id);
            if (menuItemDetails == null) return NotFound();
            return View(menuItemDetails);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var menuItemDetails = await _service.GetByIdAsync(id);
            if (menuItemDetails == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItemDetails = await _service.GetByIdAsync(id);
            if (menuItemDetails == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
