using System.ComponentModel.DataAnnotations;

namespace eSnacks.Models;

public class City
{
    public City()
    {
        Restaurants = new HashSet<Restaurant>();
    }

    [Key]
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string ZipCode { get; set; }
    
    public ICollection<Restaurant> Restaurants { get; set; }
}