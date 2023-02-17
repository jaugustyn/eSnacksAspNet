using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eSnacks.Data;
using eSnacks.Data.Services;
using eSnacks.Models;
using eSnacks.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace eSnacks.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        // GET: Restaurant
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var restaurants = await _service.GetAllRestaurantsAsync();
            return restaurants != null ? View(restaurants) : Problem("Entity set 'ApplicationDbContext.City' is null.");
        }

        
        // GET: Restaurant/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var restaurantDetails = await _service.GetRestaurantByIdAsync(id);
            if (restaurantDetails == null) return NotFound();
            return View(restaurantDetails);
        }

        
        // GET: Restaurant/Create
        public async Task<IActionResult> Create()
        {
            var restaurantDropdownsData = await _service.GetRestaurantDropdownsValues();
            ViewBag.CityId = new SelectList(restaurantDropdownsData.Cities, "Id", "CityName");
            
            return View();
        }
        
        // POST: Restaurant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantName,Address,Description,CityId")]RestaurantVM restaurant)
        {
            if (!ModelState.IsValid)
            {
                var restaurantDropdownsData = await _service.GetRestaurantDropdownsValues();
                ViewBag.CityId = new SelectList(restaurantDropdownsData.Cities, "Id", "CityName");
                
                return View(restaurant);
            }
            
            await _service.AddRestaurantAsync(restaurant);
            return RedirectToAction(nameof(Index));
        }
        

        // GET: Restaurant/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var restaurantDetails = await _service.GetRestaurantByIdAsync(id);
            if (restaurantDetails == null) return NotFound();
            
            var restaurantDropdownsData = await _service.GetRestaurantDropdownsValues();
            ViewBag.CityId = new SelectList(restaurantDropdownsData.Cities, "Id", "CityName");

            var restaurantDetailsVM = new RestaurantVM()
            {
                Id = restaurantDetails.Id,
                RestaurantName = restaurantDetails.RestaurantName,
                Address = restaurantDetails.Address,
                Description = restaurantDetails.Description,
                CityId = restaurantDetails.CityId,
            };
            
            return View(restaurantDetailsVM);
        }
        
        // POST: Restaurant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RestaurantName,Address,Description,CityId")] RestaurantVM restaurant)
        {
            if (id != restaurant.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                var restaurantDropdownsData = await _service.GetRestaurantDropdownsValues();
                ViewBag.CityId = new SelectList(restaurantDropdownsData.Cities, "Id", "CityName");
                
                return View(restaurant);
            }
            
            try
            {
                await _service.UpdateRestaurantAsync(restaurant);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetRestaurantByIdAsync(id) == null)
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

        
        // GET: Restaurant/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var restaurantDetails = await _service.GetByIdAsync(id);
            if (restaurantDetails == null) return NotFound();
            return View(restaurantDetails);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var restaurantDetails = await _service.GetByIdAsync(id);
            if (restaurantDetails == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurantDetails = await _service.GetByIdAsync(id);
            if (restaurantDetails == null) return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> FindRestaurantsInCity(string cityName)
        {
            var restaurants = await _service.GetRestaurantsInCityAsync(cityName);
            
            if (!restaurants.Any())
            {
                TempData["NotFound"] = cityName;
                return RedirectToAction("Index", "Home");
            }
            
            TempData["CityName"] = cityName;
            return View("Index", restaurants);
        }
    }
}
