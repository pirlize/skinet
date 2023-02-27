
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // A DbContext instance represents a session with the database and can be used to 
    // query and save instances of your entities. DbContext is a combination of the Unit Of Work and Repository patterns.
    public class StoreContext : DbContext
    {
        //options is the connection string
        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        // Now we set entities, Products will be name of the table. columns properties
        public DbSet<Product> Products { get; set; }
        //now that we added types and brands, when we migrate, products table will have 2 foreign keys for types and brands
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        //we pass our own configuration by overriding method from base and passing it via assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}