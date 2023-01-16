using eSnacks.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eSnacks.Models;

namespace eSnacks.Data;

public class ApplicationDbContext : IdentityDbContext<eSnacksUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<eSnacks.Models.City>? City { get; set; }
    public DbSet<eSnacks.Models.Restaurant>? Restaurant { get; set; }
}