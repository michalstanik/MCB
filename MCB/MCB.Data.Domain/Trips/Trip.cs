﻿using MCB.Data.Domain.Geo;
using MCB.Data.Domain.User;
using MCB.Data.Domain.WorldHeritages;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<TUser> Users()
        {
            var users = new List<TUser>();
            foreach (var join in UserTrips)
            {
                users.Add(join.TUser);
            }
            return users;
        }

        public IEnumerable<Country> Countries()
        {
            var countries = new List<Country>();

            foreach (var stop in Stops)
            {
                if (!countries.Contains(stop.Country))
                {
                    countries.Add(stop.Country);
                }
            }
            return countries;
        }

        public IEnumerable<WorldHeritage> WorldHeritages()
        {
            var worlHeritage = new List<WorldHeritage>();

            foreach (var stop in Stops)
            {
                if(stop.WorldHeritageId != null && !worlHeritage.Contains(stop.WorldHeritage))
                {
                    worlHeritage.Add(stop.WorldHeritage);
                }
            }

            return worlHeritage;
        }

        //TODO: Add new fetures: 1. Year of Trip (start) 2. TripStart and End Date 3. Number of days        
        public IDictionary<string, int> Statistics()
        {
            var statistics = new Dictionary<string, int>
            {
                { "countriesCount", Countries().Count() },
                { "stopsCount", Stops.Count() },
                { "userCount", Users().Count() },
                { "worldHeritages", WorldHeritages().Count() },
                { "yearOfTrip", Stops.Select(s => s.Arrival.Year).Max() },
                { "numberOfDays", System.Convert.ToInt32(System.Math.Floor((Stops.Select(s => s.Departure).Max() - Stops.Select(s => s.Arrival).Min()).TotalDays)) }
            };

            return statistics;
        }

        public IDictionary<string, DateTime> Dates()
        {
            var dates = new Dictionary<string, DateTime>
            {
                { "startDate", Stops.Select(s => s.Arrival.Date).Min() },
                { "endDate", Stops.Select(s => s.Departure.Date).Max() }
            };

            return dates;
        }
    }
}
