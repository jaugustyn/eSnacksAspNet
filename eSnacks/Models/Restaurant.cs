namespace eSnacks.Models;

public class Restaurant
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }
    
    public City City { get; set; }
}