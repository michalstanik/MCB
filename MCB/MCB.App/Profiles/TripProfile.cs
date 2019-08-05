using AutoMapper;

namespace MCB.App.Profiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Data.Domain.Trips.Trip, Business.Models.Trips.TripModel>();
        }
    }
}
