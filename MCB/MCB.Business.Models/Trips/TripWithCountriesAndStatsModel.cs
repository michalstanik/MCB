using System.Collections.Generic;

namespace MCB.Business.Models.Trips
{
    public class TripWithCountriesAndStatsModel : TripWithCountriesModel
    {
        public Dictionary<string, int> Statistics = new Dictionary<string, int>();
    }
}
