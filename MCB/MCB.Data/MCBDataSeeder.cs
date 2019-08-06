using MCB.Data.Domain.Trips;
using MCB.Data.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCB.Data
{
    public class MCBDataSeeder
    {
        private readonly MCBContext _context;

        public MCBDataSeeder(MCBContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            var firstUser = new TUser() { Id = "fec0a4d6-5830-4eb8-8024-272bd5d6d2bb", UserName = "Michał" };

            var countryAzerbaijan = _context.Country.Where(c => c.Alpha3Code == "AZE").FirstOrDefault();

            var azerbaijanTrip = new Trip()
            {
                Name = "My First Azerbaijan Trip",
                TripManager = firstUser,
                UserTrips = new List<UserTrip>()
                {
                    new UserTrip() { TUser = firstUser}
                },
                Stops = new List<Stop>()
                {
                    new Stop()
                    {
                        Name = "Baku",
                        Description = "First Day in Baku",
                        Order = 1,
                        Arrival = new DateTime(2016, 5, 1),
                        Departure = new DateTime(2016, 5, 2),
                        Country = countryAzerbaijan,
                        Latitude = 40.383333,
                        Longitude = 49.866667,
                        WorldHeritage = _context.WorldHeritage.Where(w => w.UnescoId == "1076").FirstOrDefault()
                    }
                }
            };
            _context.AddRange(azerbaijanTrip);
            await _context.SaveChangesAsync();
        }
    }
}
