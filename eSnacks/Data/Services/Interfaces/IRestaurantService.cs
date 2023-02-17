using eSnacks.Data.Base;
using eSnacks.Models;
using eSnacks.Models.ViewModels;

namespace eSnacks.Data.Services;

public interface IRestaurantService:IEntityBaseRepository<Restaurant>
{
    Task<ICollection<Restaurant>> GetAllRestaurantsAsync();
    Task<Restaurant> GetRestaurantByIdAsync(int id);
    Task<ICollection<Restaurant>> GetRestaurantsInCityAsync(string cityName);
    Task<RestaurantDropdownsVM> GetRestaurantDropdownsValues();
    Task AddRestaurantAsync(RestaurantVM data);
    Task UpdateRestaurantAsync(RestaurantVM data);
}