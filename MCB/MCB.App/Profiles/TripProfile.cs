﻿using AutoMapper;

namespace MCB.App.Profiles
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Data.Domain.Trips.Trip, Business.Models.Trips.TripModel>();
            CreateMap<Data.Domain.Trips.Trip, Business.Models.Trips.TripWithStopsModel>();
            CreateMap<Data.Domain.Trips.Trip, Business.Models.Trips.TripWithStopsAndUsersModel>();
            CreateMap<Data.Domain.Trips.Trip, Business.Models.Trips.TripWithCountriesModel>();
            CreateMap<Data.Domain.Trips.Trip, Business.Models.Trips.TripWithCountriesAndStatsModel>();

            CreateMap<Data.Domain.Trips.Stop, Business.Models.Trips.StopModel>();

            CreateMap<Data.Domain.User.TUser, Business.Models.Users.TUserModel>();

            CreateMap<Data.Domain.Geo.Country, Business.Models.Geo.CountryModel>();
        }
    }
}
