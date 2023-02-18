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

        if(userRole != "Administrator")
        {
            orders = orders.Where(n => n.UserId == userId).ToList();
        }

        return orders;
    }

    public async Task StoreOrderAsync(List<ShoppingCartItem> items, double price, string userId, string userEmailAddress)
    {
        var orderStatus = new OrderStatus() {Status = Models.Enums.OrderStatus.Completed};
        
        await _context.OrderStatuses.AddAsync(orderStatus);
        await _context.SaveChangesAsync();
        
        // Not fully implemented TODO
        
        var order = new PlacedOrder()
        {
            UserId = userId,
            RestaurantId = 1,
            OrderTime = DateTime.Now.AddHours(Random.Shared.Next(1, 5)),
            EstimatedDeliveryTime = DateTime.Now,
            DeliveryAddress = _context.Users.FirstOrDefault(x => x.Id.Equals(userId))?.Address,
            Price = 0,
            Discount = 0,
            FinalPrice = 0,
            Comment = "Not implemented",
            OrderStatusId = orderStatus.OrderStatusId,
        };
        
        await _context.PlacedOrders.AddAsync(order);
        await _context.SaveChangesAsync();
        
        foreach (var item in items)
        {
            // item.MenuItem.
            var orderItem = new InOrder()
            {
                MenuItemId = item.MenuItem.Id,
                PlacedOrderId = order.PlacedOrderId,
                Quantity = item.Quantity,
                Price = item.MenuItem.Price,
                Comment = "No comment",
            };
            await _context.InOrders.AddAsync(orderItem);
        }
        await _context.SaveChangesAsync();
    }
}