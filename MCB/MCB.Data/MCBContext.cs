using MCB.Data.Domain.Geo;
using Microsoft.EntityFrameworkCore;

namespace MCB.Data
{
    public class MCBContext : DbContext
    {
        public MCBContext(DbContextOptions<MCBContext> options) : base(options)
        {

        }

        //GeoEntites
        public DbSet<Country> Country { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Continent> Continent { get; set; }
    }
}
