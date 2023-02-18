using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eSnacks.Data.Base;

namespace eSnacks.Models;

public class Restaurant : IEntityBase
{
    [Key]
    public int Id { get; set; }
    public string RestaurantName { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public Uri PhotoUrl { get; set; }
    public int CityId { get; set; }
    
    [ForeignKey("CityId")]
    public virtual City City { get; set; }
    
    public ICollection<MenuItem> MenuItems { get; set; }
    public ICollection<PlacedOrder> PlacedOrders { get; set; }
}