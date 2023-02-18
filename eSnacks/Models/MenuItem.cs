using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eSnacks.Data.Base;

namespace eSnacks.Models;

public class MenuItem:IEntityBase
{
    [Key]
    public int Id { get; set; }
    
    public string ItemName { get; set; }
    public string Description { get; set; }
    public Uri PhotoUrl { get; set; }
    public string Ingredients { get; set; }
    public double Price { get; set; }
    public bool Available { get; set; }
    
    public int CategoryId { get; set; }
    public int RestaurantId { get; set; }
    // public int InOrderId { get; set; }
    
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
    
    [ForeignKey("RestaurantId")]
    public virtual Restaurant Restaurant { get; set; }
    
}