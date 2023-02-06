using System.ComponentModel.DataAnnotations;

namespace eSnacks.Models;

public class OrderStatus
{
    [Key]
    public int OrderStatusId { get; set; }
    public Enums.OrderStatus Status { get; set; }
    
    public PlacedOrder PlacedOrder { get; set; }
}