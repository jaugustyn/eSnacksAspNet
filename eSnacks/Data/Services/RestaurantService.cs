using eSnacks.Data.Base;
using eSnacks.Models;
using eSnacks.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eSnacks.Data.Services;

public class RestaurantService : EntityBaseRepository<Restaurant>, IRestaurantService
{
    private readonly ApplicationDbContext _context;

    public RestaurantService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Restaurant>> GetAllRestaurantsAsync()
    {
        var restaurantDetails = await _context.Restaurants
            .Include(r => r.City)
            .Include(r => r.MenuItems)
            .ThenInclude(mi => mi.Category)
            .ToListAsync();

        return restaurantDetails;
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(int id)
    {
        var restaurantDetails = await _context.Restaurants
            .Include(r => r.City)
            .Include(r => r.MenuItems)
            .ThenInclude(mi => mi.Category)
            .FirstOrDefaultAsync(n => n.Id == id);

        return restaurantDetails;
    }

    public async Task<ICollection<Restaurant>> GetRestaurantsInCityAsync(string cityName)
    {
        var restaurants = await _context.Restaurants
            .Include(r => r.City)
            .Include(r => r.MenuItems)
            .ThenInclude(mi => mi.Category)
            .Where(x => x.City.CityName.Equals(cityName.Trim()))
            .ToListAsync();
            
        return restaurants;
    }

    public async Task AddRestaurantAsync(RestaurantVM data)
    {
        var newMovie = new Restaurant
        {
            RestaurantName = data.RestaurantName,
            Address = data.Address,
            Description = data.Description,
            CityId = data.CityId,
        };
        await _context.Restaurants.AddAsync(newMovie);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateRestaurantAsync(RestaurantVM data)
    {
        var dbRestaurant = await _context.Restaurants.FirstOrDefaultAsync(n => n.Id == data.Id);

        if (dbRestaurant != null)
        {
            dbRestaurant.RestaurantName = data.RestaurantName;
            dbRestaurant.Address = data.Address;
            dbRestaurant.Description = data.Description;
            dbRestaurant.CityId = data.CityId;

            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<RestaurantDropdownsVM> GetRestaurantDropdownsValues()
    {
        var response = new RestaurantDropdownsVM()
        {
            Cities = await _context.Cities.OrderBy(n => n.CityName).ToListAsync(),
        };

        return response;
    }
}