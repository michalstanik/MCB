using MCB.Data.Domain.User;
using System.Collections.Generic;

namespace MCB.Data.Domain.Trips
{
    public class Trip
    {
        public Trip()
        {
            UserTrips = new List<UserTrip>();
            Stops = new List<Stop>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserTrip> UserTrips { get; set; }
        public List<Stop> Stops { get; set; }

        public TUser TripManager { get; set; }
    }
}
