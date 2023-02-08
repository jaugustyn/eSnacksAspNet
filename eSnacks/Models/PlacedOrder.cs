using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSnacks.Models;

public class PlacedOrder
{
    [Key]
    public int PlacedOrderId { get; set; }
    
    public DateTime OrderTime { get; set; } = DateTime.Now;
    public DateTime EstimatedDeliveryTime { get; set; }
    public string DeliveryAddress { get; set; }
    
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalPrice { get; set; }
    
    public string Comment { get; set; }

    public int OrderStatusId { get; set; }
    public string UserId { get; set; }
    public int RestaurantId { get; set; }
    
    [ForeignKey("OrderStatusId")]
    public virtual OrderStatus OrderStatus { get; set; }
    
    [ForeignKey("CustomerId")]
    public virtual eSnacksUser User { get; set; }
    
    [ForeignKey("RestaurantId")]
    public virtual Restaurant Restaurant { get; set; }
    
    public ICollection<InOrder> InOrders { get; set; }
    // public IList<UserOrders> UserOrders { get; set; }
}