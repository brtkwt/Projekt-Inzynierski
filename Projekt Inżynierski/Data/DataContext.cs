using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;

namespace Projekt_Inżynierski.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }

    }
}
