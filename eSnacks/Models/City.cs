using System.ComponentModel.DataAnnotations;

namespace eSnacks.Models;

public class City
{
    [Key]
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string ZipCode { get; set; }
}