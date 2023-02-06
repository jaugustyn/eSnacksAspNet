namespace eSnacks.Models;

public class UserOrders
{
    public string UserId { get; set; }
    public eSnacksUser User { get; set; }
    
    public int PlacedOrderId { get; set; }
    public PlacedOrder PlacedOrder { get; set; }
}