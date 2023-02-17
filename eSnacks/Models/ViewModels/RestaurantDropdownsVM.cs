namespace eSnacks.Models.ViewModels;

public class RestaurantDropdownsVM
{
    public RestaurantDropdownsVM()
    {
        Cities = new List<City>();
    }

    public List<City> Cities { get; set; }
}