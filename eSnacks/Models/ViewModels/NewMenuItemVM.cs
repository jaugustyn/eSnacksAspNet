using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace eSnacks.Models.ViewModels;

public class NewMenuItemVM
{
    public int Id { get; set; }
    
    [DisplayName("Menu item name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The menu item name should have at least 2 characters and a maximum of 50.")]
    [Required(ErrorMessage = "Menu item name is required.")]
    public string ItemName { get; set; }
    
    [DisplayName("Description")]
    [StringLength(250, ErrorMessage = "Description should have a maximum of 250 characters.")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    
    [Display(Name = "Photo url")]
    [DataType(DataType.ImageUrl)]
    public Uri PhotoUrl { get; set; }
    
    [DisplayName("Ingredients")]
    [StringLength(250, ErrorMessage = "Ingredients should have a maximum of 250 characters.")]
    public string Ingredients { get; set; }
    
    [DisplayName("Unit price")]
    [Required(ErrorMessage = "Price is required.")]
    [DataType(DataType.Currency)]
    public double Price { get; set; }
    
    [DisplayName("Is available")]
    public bool Available { get; set; }
    
    [Display(Name = "Category")]
    [Required(ErrorMessage = "Category is required")]
    public int CategoryId { get; set; }
    
    [Display(Name = "Restaurant")]
    [Required(ErrorMessage = "Restaurant is required")]
    public int RestaurantId { get; set; }
}