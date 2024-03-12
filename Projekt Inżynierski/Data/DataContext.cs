using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Order;

namespace Projekt_Inżynierski.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            
            modelBuilder.Entity<Product>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Cost).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Product>().Property(p => p.ImagePath).IsRequired();

            modelBuilder.Entity<AppUser>(a => a.HasIndex(u => u.NormalizedEmail).IsUnique());

            modelBuilder.Entity<OrderItem>().OwnsOne(o => o.ProductOrdered, po => po.WithOwner() );
            modelBuilder.Entity<OrderItem>().Property(o => o.Cost).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ShippingMethod>().Property(m => m.ShippingFee).HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Order>().OwnsOne(o => o.OrderShippingAddress, o => o.WithOwner() );
            modelBuilder.Entity<Order>().Navigation(o => o.OrderShippingAddress).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderStatus).HasConversion( o => o.ToString(), o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o)) ;
            modelBuilder.Entity<Order>().HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>().Property(o => o.SumCost).HasColumnType("decimal(18,2)");

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Client",
                    NormalizedName = "CLIENT"
                },

                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
