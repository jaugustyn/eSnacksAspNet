using eSnacks.Data.Cart;

namespace eSnacks.Models.ViewModels;

public class ShoppingCartVM
{
    public ShoppingCart ShoppingCart { get; set; }
    public double ShoppingCartTotal { get; set; }
}