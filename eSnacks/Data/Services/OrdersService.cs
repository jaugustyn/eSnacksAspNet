using eSnacks.Models;
using Microsoft.EntityFrameworkCore;

namespace eSnacks.Data.Services;

public class OrdersService : IOrdersService
{
    private readonly ApplicationDbContext _context;
    public OrdersService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlacedOrder>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
    {
        var orders = await _context.PlacedOrders.Include(po => po.InOrders).ThenInclude(io => io.MenuItem).Include(po => po.User).ToListAsync();

        if(userRole != "Admin")
        {
            orders = orders.Where(n => n.UserId == userId).ToList();
        }

        return orders;
    }

    public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
    {
        var order = new PlacedOrder()
        {
            UserId = userId,
            // Email = userEmailAddress
        };
        await _context.PlacedOrders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var item in items)
        {
            var orderItem = new InOrder()
            {
                Quantity = item.Amount,
                MenuItemId = item.MenuItem.Id,
                PlacedOrderId = order.PlacedOrderId,
                Price = item.MenuItem.Price
            };
            await _context.InOrders.AddAsync(orderItem);
        }
        await _context.SaveChangesAsync();
    }
}