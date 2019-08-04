﻿using MCB.Data.Domain.Trips;
using System.Threading.Tasks;

namespace MCB.Data.RepositoriesInterfaces
{
    public interface ITripRepository
    {
        //General
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Get
        Task<Trip> GetTrip(int tripId, bool includeStops = false, bool includeUsers = false);
    }
}
