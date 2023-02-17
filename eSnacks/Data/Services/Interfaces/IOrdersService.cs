using eSnacks.Models;

namespace eSnacks.Data.Services;

public interface IOrdersService
{
    Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
    Task<List<PlacedOrder>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
}