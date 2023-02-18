using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace eSnacks.Models.ViewModels;

public class RestaurantVM
{
    public int Id { get; set; }
    
    [Display(Name = "Restaurant name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The restaurant name should have at least 2 characters and a maximum of 50.")]
    [Required(ErrorMessage = "Restaurant name is required")]
    public string RestaurantName { get; set; }

    [Display(Name = "Address")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Address should have at least 2 characters and a maximum of 100.")]
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    
    [Display(Name = "Photo url")]
    [DataType(DataType.ImageUrl)]
    public Uri PhotoUrl { get; set; }
    
    [Display(Name = "Description")]
    [StringLength(250, ErrorMessage = "The zip code should have a maximum of 250 characters.")]
    [Required(ErrorMessage = "Description is required")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    
    [Display(Name = "City")]
    [Required(ErrorMessage = "City is required")]
    public int CityId { get; set; }
}