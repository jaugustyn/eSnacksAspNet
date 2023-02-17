namespace eSnacks.Models.ViewModels;

public class NewMenuItemVM
{
    public int Id { get; set; }
    
    public string ItemName { get; set; }
    public string Description { get; set; }
    public Uri PhotoUrl { get; set; }
    public string Ingredients { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
    
    public int CategoryId { get; set; }
    public int RestaurantId { get; set; }
}