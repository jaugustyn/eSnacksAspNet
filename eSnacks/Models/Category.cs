using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using eSnacks.Data.Base;

namespace eSnacks.Models;

public class Category: IEntityBase
{
    [Key]
    public int Id { get; set; }
    
    [DisplayName("Category name")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "The category name should have at least 2 characters and a maximum of 50.")]
    [Required(ErrorMessage = "Category name is required.")]
    public string CategoryName { get; set; }
    
    [DisplayName("Description")]
    [StringLength(250, ErrorMessage = "Description should have a maximum of 250 characters.")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    
    public ICollection<MenuItem> MenuItems { get; set; }
}