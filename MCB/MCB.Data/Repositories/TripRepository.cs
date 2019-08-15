using System.Linq;
using System.Threading.Tasks;
using MCB.Data.Domain.Trips;
using MCB.Data.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace MCB.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly MCBContext _context;

        public TripRepository(MCBContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<bool> CheckUserPermissionsForTrip(int tripId, string userId)
        {
            var trip = await _context.Trip.Where(t => t.Id == tripId).FirstOrDefaultAsync();

            if (trip != null)
            {
                foreach (var user in trip.Users())
                {
                    if (user.Id == userId) return true;
                }
            }
            return false;
        }

        public async Task<Trip> GetTrip(int tripId, bool includeStops = false, bool includeUsers = false)
        {
            IQueryable<Trip> query = _context.Trip.Where(t => t.Id == tripId);

            if (includeStops && !includeUsers)
            {
                query = query
                    .Include(c => c.Stops)
                        .ThenInclude(c => c.Country)
                        .ThenInclude(r => r.Region)
                        .ThenInclude(e => e.Continent)
                    .Include(e => e.Stops)
                        .ThenInclude(e => e.WorldHeritage);
            }
            else if (!includeStops && includeUsers)
            {
                query = query.Include(c => c.UserTrips)
                    .ThenInclude(pc => pc.TUser);
            }
            else if (includeStops && includeUsers)
            {
                query = query
                    .Include(c => c.Stops)
                        .ThenInclude(c => c.Country)
                        .ThenInclude(r => r.Region)
                        .ThenInclude(e => e.Continent)
                    .Include(e => e.Stops)
                        .ThenInclude(e => e.WorldHeritage)
                    .Include(c => c.UserTrips)
                        .ThenInclude(pc => pc.TUser);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
