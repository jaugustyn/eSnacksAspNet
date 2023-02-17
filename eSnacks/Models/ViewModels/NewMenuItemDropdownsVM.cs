namespace eSnacks.Models.ViewModels;

public class NewMenuItemDropdownsVM
{
    public NewMenuItemDropdownsVM()
    {
        Categories = new List<Category>();
        Restaurants = new List<Restaurant>();
    }

    public List<Category> Categories { get; set; }
    public List<Restaurant> Restaurants { get; set; }
}