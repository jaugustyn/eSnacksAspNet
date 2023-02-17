using eSnacks.Data.Base;
using eSnacks.Models;

namespace eSnacks.Data.Services;

public class CategoryService: EntityBaseRepository<Category>, ICategoryService
{
    public CategoryService(ApplicationDbContext context) : base(context)
    {
        
    }
}