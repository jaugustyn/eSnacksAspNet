using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using eSnacks.Data.Base;

namespace eSnacks.Models;

public class City: IEntityBase
{
    [Key]
    public int Id { get; set; }
    
    [DisplayName("City name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The city name should have at least 2 characters and a maximum of 50.")]
    [Required(ErrorMessage = "City Name is required.")]
    public string CityName { get; set; }
    
    [DisplayName("Zip code")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "The zip code should have at least 2 characters and a maximum of 20.")]
    [Required(ErrorMessage = "Zip code is required.")]
    public string ZipCode { get; set; }
    
    public ICollection<Restaurant> Restaurants { get; set; }
}