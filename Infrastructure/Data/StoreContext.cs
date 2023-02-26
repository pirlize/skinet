
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
        // Now we set entities, Products will be name of table.
        public DbSet<Product> Products { get; set; }
    }
}