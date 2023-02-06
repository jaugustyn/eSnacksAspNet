using System.ComponentModel.DataAnnotations;

namespace eSnacks.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string? Description { get; set; }
    
    public ICollection<MenuItem> MenuItems { get; set; }
}