using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eSnacks.Data;
using eSnacks.Models;
using eSnacks.Models.ViewModels;

namespace eSnacks.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Restaurants
                .Include(r => r.City).Include(r => r.MenuItems).ThenInclude(mi => mi.Category);
            
        return View(await applicationDbContext.ToListAsync());
        }

        // GET: Restaurant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.City)
                .Include(x => x.MenuItems)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurant/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Cities, "Id", "CityName");
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RestaurantName,Address,Description,Id")] RestaurantVM restaurant)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant()
                {
                    RestaurantName = restaurant.RestaurantName,
                    Address = restaurant.Address,
                    Description = restaurant.Description,
                    CityId = restaurant.CityId
                };
                
                _context.Add(newRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Cities, "Id", "CityName", restaurant.CityId);
            return View(restaurant);
        }

        // GET: Restaurant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var response = new RestaurantVM()
            {
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.RestaurantName,
                Address = restaurant.Address,
                Description = restaurant.Description,
                CityId = restaurant.CityId
            };

            ViewData["Id"] = new SelectList(_context.Cities, "Id", "CityName", restaurant.CityId);
            return View(response);
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RestaurantName,Address,Description,Id")] RestaurantVM restaurant)
        {
            
            var dbRestaurant = await _context.Restaurants.FirstOrDefaultAsync(n => n.Id == restaurant.RestaurantId);
            if (id != restaurant.RestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbRestaurant.RestaurantName = restaurant.RestaurantName;
                    dbRestaurant.Address = restaurant.Address;
                    dbRestaurant.Description = restaurant.Description;
                    dbRestaurant.CityId = restaurant.CityId;

                    _context.Update(dbRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantId))
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
            ViewData["Id"] = new SelectList(_context.Cities, "Id", "CityName", restaurant.CityId);
            return View(restaurant);
        }

        // GET: Restaurant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restaurants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Restaurant'  is null.");
            }
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
          return (_context.Restaurants?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> SearchRestaurants(string cityName)
        {
            cityName = cityName.Trim();
            var searchCities = await _context.Restaurants.Include(r => r.City).Include(r => r.MenuItems).ThenInclude(mi => mi.Category)
                .Where(x => x.City.CityName.Equals(cityName)).ToListAsync();
            
            if (searchCities.Count == 0)
            {
                TempData["NotFound"] = cityName;
                return RedirectToAction("Index", "Home");
            }
            
            TempData["CityName"] = cityName;
            return View("Index", searchCities);
        }
    }
}
