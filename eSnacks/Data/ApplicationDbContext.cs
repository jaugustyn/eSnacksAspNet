using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eSnacks.Models;
using OrderStatus = eSnacks.Models.Enums.OrderStatus;

namespace eSnacks.Data;

public class ApplicationDbContext : IdentityDbContext<eSnacksUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<eSnacks.Models.City> Cities { get; set; }
    public DbSet<eSnacks.Models.Restaurant> Restaurants { get; set; }
    public DbSet<eSnacks.Models.PlacedOrder> PlacedOrders { get; set; }
    public DbSet<eSnacks.Models.UserOrders> UserOrders { get; set; }
    public DbSet<eSnacks.Models.OrderStatus> OrderStatuses { get; set; }
    public DbSet<eSnacks.Models.MenuItem> MenuItems { get; set; }
    public DbSet<eSnacks.Models.Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // City
        modelBuilder.Entity<City>(b =>
        {
            b.HasKey(c => c.CityId);
            b.HasMany<Restaurant>(c => c.Restaurants).WithOne(c => c.City)
                .HasForeignKey(c => c.RestaurantId).OnDelete(DeleteBehavior.NoAction);
        });

        // Restaurant
        modelBuilder.Entity<Restaurant>(b =>
        {
            b.HasKey(r => r.RestaurantId);
            b.HasOne<City>(r => r.City).WithMany(c => c.Restaurants)
                .HasForeignKey(r => r.CityId).OnDelete(DeleteBehavior.NoAction).IsRequired();
        });

        // Placed Order
        modelBuilder.Entity<PlacedOrder>(b =>
        {
            b.HasKey(po => po.PlacedOrderId);
            b.HasMany<InOrder>(p => p.InOrders).WithOne(p => p.PlacedOrder)
                .HasForeignKey(p => p.InOrderId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne<Models.OrderStatus>(po => po.OrderStatus).WithOne(os => os.PlacedOrder)
                .HasForeignKey<PlacedOrder>(po => po.OrderStatusId).IsRequired();
        });
            
        // Placed Order by Users (many to many)
        modelBuilder.Entity<UserOrders>(b =>
        {
            b.HasKey(uo => new {uo.UserId, uo.PlacedOrderId});
            b.HasOne<eSnacksUser>(uo => uo.User).WithMany(u => u.UserOrders)
                .HasForeignKey(uo => uo.UserId).IsRequired();
            b.HasOne<PlacedOrder>(uo => uo.PlacedOrder).WithMany(po => po.UserOrders)
                .HasForeignKey(uo => uo.PlacedOrderId).IsRequired();
        });
        
        // Order Status
        modelBuilder.Entity<Models.OrderStatus>(b =>
        {
            b.HasKey(os => os.OrderStatusId);
            b.HasOne<PlacedOrder>(os => os.PlacedOrder).WithOne(po => po.OrderStatus)
                .HasForeignKey<PlacedOrder>(po => po.OrderStatusId).IsRequired();
            
            //Shadow properties
            b.Property<DateTime>("CreatedDate");
            b.Property<DateTime>("UpdatedDate");
        });

        // Menu Item
        modelBuilder.Entity<MenuItem>(b =>
        {
            b.HasKey(mi => mi.MenuItemId);

            b.HasOne<Category>(mi => mi.Category).WithMany(c => c.MenuItems)
                .HasForeignKey(mi => mi.CategoryId).IsRequired();
        });
        
        // Menu Category
        modelBuilder.Entity<Category>(b =>
        {
            b.HasKey(c => c.CategoryId);
            
            b.HasMany<MenuItem>(c => c.MenuItems).WithOne(mi => mi.Category)
                .HasForeignKey(mi => mi.CategoryId).OnDelete(DeleteBehavior.SetNull);
        });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}