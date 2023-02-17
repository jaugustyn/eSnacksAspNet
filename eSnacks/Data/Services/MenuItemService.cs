using eSnacks.Data.Base;
using eSnacks.Models;
using eSnacks.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eSnacks.Data.Services;

public class MenuItemService : EntityBaseRepository<MenuItem>, IMenuItemService
{
    private readonly ApplicationDbContext _context;

    public MenuItemService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddNewMenuItemAsync(NewMenuItemVM data)
    {
        var newMovie = new MenuItem
        {
            ItemName = data.ItemName,
            Description = data.Description,
            PhotoUrl = data.PhotoUrl,
            Ingredients = data.Ingredients,
            Price = data.Price,
            Available = data.Available,
            CategoryId = data.CategoryId,
            RestaurantId = data.RestaurantId
        };
        await _context.MenuItems.AddAsync(newMovie);
        await _context.SaveChangesAsync();
    }

    public async Task<MenuItem> GetMenuItemByIdAsync(int id)
    {
        var movieDetails = await _context.MenuItems
            .Include(mi => mi.Category)
            .Include(mi => mi.Restaurant)
            .FirstOrDefaultAsync(n => n.Id == id);

        return movieDetails;
    }

    public async Task UpdateMenuItemAsync(NewMenuItemVM data)
    {
        var dbMenuItem = await _context.MenuItems.FirstOrDefaultAsync(n => n.Id == data.Id);

        if (dbMenuItem != null)
        {
            dbMenuItem.ItemName = data.ItemName;
            dbMenuItem.Description = data.Description;
            dbMenuItem.PhotoUrl = data.PhotoUrl;
            dbMenuItem.Ingredients = data.Ingredients;
            dbMenuItem.Price = data.Price;
            dbMenuItem.Available = data.Available;
            dbMenuItem.CategoryId = data.CategoryId;
            dbMenuItem.RestaurantId = data.RestaurantId;

            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<NewMenuItemDropdownsVM> GetNewMenuItemDropdownsValues()
    {
        var response = new NewMenuItemDropdownsVM
        {
            Categories = await _context.Categories.OrderBy(n => n.CategoryName).ToListAsync(),
            Restaurants = await _context.Restaurants.OrderBy(n => n.RestaurantName).ToListAsync()
        };

        return response;
    }
}