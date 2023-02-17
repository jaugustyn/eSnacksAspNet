using System.ComponentModel.DataAnnotations;
using eSnacks.Data.Base;

namespace eSnacks.Models;

public class Category: IEntityBase
{
    [Key]
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    
    public ICollection<MenuItem> MenuItems { get; set; }
}