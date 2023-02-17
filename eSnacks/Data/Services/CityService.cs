using eSnacks.Data.Base;
using eSnacks.Models;

namespace eSnacks.Data.Services;

public class CityService: EntityBaseRepository<City>, ICityService
{
    public CityService(ApplicationDbContext context) : base(context)
    {
        
    }
}