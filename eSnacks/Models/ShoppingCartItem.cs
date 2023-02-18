using System.ComponentModel.DataAnnotations;

namespace eSnacks.Models;

public class ShoppingCartItem
{
    
    [Key]
    public int Id { get; set; }

    public MenuItem MenuItem { get; set; }
    public int Quantity { get; set; }


    public string ShoppingCartId { get; set; }
}