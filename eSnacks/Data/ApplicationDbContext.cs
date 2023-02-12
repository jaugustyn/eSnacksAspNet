using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eSnacks.Models;
using Humanizer;
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
    public DbSet<eSnacks.Models.OrderStatus> OrderStatuses { get; set; }
    public DbSet<eSnacks.Models.MenuItem> MenuItems { get; set; }
    public DbSet<eSnacks.Models.InOrder> InOrders { get; set; }
    public DbSet<eSnacks.Models.Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        // User
        modelBuilder.Entity<eSnacksUser>(b =>
        {
            //Additional relation
            b.HasMany<PlacedOrder>(u => u.PlacedOrders).WithOne(po => po.User)
                .HasForeignKey(po => po.UserId);
        });
        
        // City
        modelBuilder.Entity<City>(b =>
        {
            b.HasKey(c => c.CityId);
            b.HasMany<Restaurant>(c => c.Restaurants).WithOne(c => c.City)
                .HasForeignKey(c => c.RestaurantId).OnDelete(DeleteBehavior.NoAction);
            b.Property(c => c.CityName).UseCollation("NOCASE");
        });

        // Restaurant
        modelBuilder.Entity<Restaurant>(b =>
        {
            b.HasKey(r => r.RestaurantId);
            b.HasOne<City>(r => r.City).WithMany(c => c.Restaurants)
                .HasForeignKey(r => r.CityId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            b.HasMany<MenuItem>(r => r.MenuItems).WithOne(mi => mi.Restaurant)
                .HasForeignKey(mi => mi.RestaurantId);
            b.HasMany<PlacedOrder>(r => r.PlacedOrders).WithOne(po => po.Restaurant)
                .HasForeignKey(po => po.RestaurantId);
        });

        // Placed Order
        modelBuilder.Entity<PlacedOrder>(b =>
        {
            b.HasKey(po => po.PlacedOrderId);
            b.HasOne<Models.OrderStatus>(po => po.OrderStatus).WithOne(os => os.PlacedOrder)
                .HasForeignKey<PlacedOrder>(po => po.OrderStatusId).IsRequired();
            b.HasOne<Restaurant>(po => po.Restaurant).WithMany(r => r.PlacedOrders)
                .HasForeignKey(po => po.RestaurantId);
            b.HasOne<eSnacksUser>(po => po.User).WithMany(u => u.PlacedOrders)
                .HasForeignKey(po => po.UserId);
            b.HasMany<InOrder>(po => po.InOrders).WithOne(io => io.PlacedOrder)
                .HasForeignKey(io => io.PlacedOrderId).OnDelete(DeleteBehavior.Cascade);
        });
        
        // In Order
        modelBuilder.Entity<InOrder>(b =>
        {
            b.HasKey(io => io.InOrderId);
            b.HasOne<PlacedOrder>(io => io.PlacedOrder).WithMany(po => po.InOrders)
                .HasForeignKey(io => io.PlacedOrderId).IsRequired();
            // b.HasOne(io => io.MenuItem).WithOne(x => x.Restaurant)
        });

        // Order Status
        modelBuilder.Entity<Models.OrderStatus>(b =>
        {
            b.HasKey(os => os.OrderStatusId);
            b.HasOne<PlacedOrder>(os => os.PlacedOrder).WithOne(po => po.OrderStatus)
                .HasForeignKey<PlacedOrder>(po => po.OrderStatusId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            
            //Shadow properties
            b.Property<DateTime>("CreatedDate");
            b.Property<DateTime>("UpdatedDate");
        });

        // Menu Item
        modelBuilder.Entity<MenuItem>(b =>
        {
            b.HasKey(mi => mi.MenuItemId);

            b.HasOne<Category>(mi => mi.Category).WithMany(c => c.MenuItems)
                .HasForeignKey(mi => mi.CategoryId).OnDelete(DeleteBehavior.SetNull).IsRequired();
            b.HasOne<Restaurant>(mi => mi.Restaurant).WithMany(r => r.MenuItems).HasForeignKey(mi => mi.RestaurantId);
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