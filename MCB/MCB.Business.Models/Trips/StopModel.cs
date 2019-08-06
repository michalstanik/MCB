using System;

namespace MCB.Business.Models.Trips
{
    public class StopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }

        public int TripId { get; set; }


        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string CountryName { get; set; }
        public string CountryAlpha2Code { get; set; }
        public string CountryAlpha3Code { get; set; }
        public long CountryArea { get; set; }
        public string CountryRegionName { get; set; }

    }
}
