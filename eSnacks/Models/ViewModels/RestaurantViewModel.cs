using System.ComponentModel.DataAnnotations;

namespace eSnacks.Models.ViewModels;

public class RestaurantViewModel
{
    public int RestaurantId { get; set; }
    
    [Display(Name = "Restaurant name")]
    [Required(ErrorMessage = "Restaurant name is required")]
    public string Name { get; set; }

    [Display(Name = "Address")]
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    
    [Display(Name = "Description")]
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    
    [Display(Name = "City")]
    [Required(ErrorMessage = "City is required")]
    public int CityId { get; set; }
}