using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSnacks.Models;

public class InOrder
{
    [Key]
    public int InOrderId { get; set; }
    
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Comment { get; set; }
    
    public int PlacedOrderId { get; set; }
    public int MenuItemId { get; set; }
    
    [ForeignKey("PlacedOrderId")]
    public virtual PlacedOrder PlacedOrder { get; set; }
    
    [ForeignKey("MenuItemId")]
    public virtual MenuItem MenuItem { get; set; }
    
}