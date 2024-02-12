using Microsoft.EntityFrameworkCore;

namespace CRUDBEER.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) 
            :base(options)
        {
            Console.WriteLine("hola");
        }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brand> Brands { get; set; }

    }
}
