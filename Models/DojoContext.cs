using Microsoft.EntityFrameworkCore;
 
namespace DojoLeague.Models
{
    public class DojoContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DojoContext(DbContextOptions<DojoContext> options) : base(options) { }

        public DbSet<Dojo> dojos {get;set;}
        public DbSet<Ninja> ninjas {get;set;}
    }
}