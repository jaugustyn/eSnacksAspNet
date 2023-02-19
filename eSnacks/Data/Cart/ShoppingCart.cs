using eSnacks.Models;
using Microsoft.EntityFrameworkCore;

namespace eSnacks.Data.Cart;


public class ShoppingCart
{
    private readonly ApplicationDbContext _context;

    private string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ShoppingCart(ApplicationDbContext context)
    {
        _context = context;
    }

    public static ShoppingCart GetShoppingCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<ApplicationDbContext>();

        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }

    public void AddItemToCart(MenuItem movie)
    {
        var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.MenuItem.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

        if(shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
                MenuItem = movie,
                Quantity = 1
            };

            _context.ShoppingCartItems.Add(shoppingCartItem);
        } else
        {
            shoppingCartItem.Quantity++;
        }
        _context.SaveChanges();
    }

    public void RemoveItemFromCart(MenuItem menuItem)
    {
        var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.MenuItem.Id == menuItem.Id && n.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem != null)
        {
            if(shoppingCartItem.Quantity > 1)
            {
                shoppingCartItem.Quantity--;
            } else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }
        _context.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??= _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.MenuItem).ThenInclude(mi => mi.Restaurant).ToList();
    }

    public double GetShoppingCartTotal() =>  _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => (double)n.MenuItem.Price * n.Quantity).Sum();

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
        _context.ShoppingCartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
