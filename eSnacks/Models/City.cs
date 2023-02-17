using System.ComponentModel.DataAnnotations;
using eSnacks.Data.Base;

namespace eSnacks.Models;

public class City: IEntityBase
{
    public City()
    {
        Restaurants = new HashSet<Restaurant>();
    }

    [Key]
    public int Id { get; set; }
    public string CityName { get; set; }
    public string ZipCode { get; set; }
    
    public ICollection<Restaurant> Restaurants { get; set; }
}