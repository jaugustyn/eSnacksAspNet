using eSnacks.Data.Base;
using eSnacks.Models;
using eSnacks.Models.ViewModels;

namespace eSnacks.Data.Services;

public interface IMenuItemService:IEntityBaseRepository<MenuItem>
{
    Task<MenuItem> GetMenuItemByIdAsync(int id);
    Task<NewMenuItemDropdownsVM> GetNewMenuItemDropdownsValues();
    Task AddNewMenuItemAsync(NewMenuItemVM data);
    Task UpdateMenuItemAsync(NewMenuItemVM data);
}