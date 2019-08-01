using MCB.Data.Domain.Geo;
using MCB.Data.Domain.User;
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

        //User
        public DbSet<TUser> TUser { get; set; }
        public DbSet<UserCountry> UserCountry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //UserCountry
            modelBuilder.Entity<UserCountry>().Property(c => c.CountryKnowledgeType).HasConversion<string>();
            modelBuilder.Entity<UserCountry>().HasKey(s => new { s.CountryId, s.TUserId });
        }
    }
}
