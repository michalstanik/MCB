using MCB.Data;
using MCB.Data.Domain.Geo;
using MCB.Data.Domain.Trips;
using MCB.Data.Repositories;
using MCB.Data.RepositoriesInterfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MCB.Tests.Data
{
    public class CountryRepositoryTests
    {
        public CountryRepositoryTests()
        {

        }

        [Fact]
        public async Task GetCountriesForTrip_TripWithNoStops_EmptyListOfCountries()
        {
            var dbOptions = GetDbOptions();
            //Arrange
            using (var context = new MCBContext(dbOptions))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Add(new Trip()
                {
                    Id = 8,
                    Name = "Trip 1"
                });
                await context.SaveChangesAsync();

            }

            using (var context = new MCBContext(dbOptions))
            {
                var geoRepository = new GeoRepository(context);

                // Act
                var countries = await geoRepository.GetCountriesForTrip(8);

                // Assert
                Assert.Empty(countries);
            }
        }

        [Fact]
        public async Task GetCountriesForTrip_TripWithOneStops_ListOfOneCountry()
        {
            var dbOptions = GetDbOptions();
            //Arrange
            using (var context = new MCBContext(dbOptions))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var trip1 = new Trip()
                {
                    Id = 8,
                    Name = "Trip 1",
                    Stops = new List<Stop>()
                    {
                      new Stop() { Name = "Stop 1" },
                      new Stop() { Name = "Stop 2", CountryId = 5, Country = new Country{ Id = 5, Name = "Poland" } }
                    }
                };

                context.Add(trip1);           
                await context.SaveChangesAsync();

            }

            using (var context = new MCBContext(dbOptions))
            {
                var geoRepository = new GeoRepository(context);

                // Act
                var countries = await geoRepository.GetCountriesForTrip(8);

                // Assert
                Assert.Single(countries);
            }
        }

        [Fact]
        public async Task GetCountriesForTrip_TripWithTwoStopsAndOneCountry_ListOfOneCountry()
        {
            var dbOptions = GetDbOptions();
            //Arrange
            using (var context = new MCBContext(dbOptions))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var trip1 = new Trip()
                {
                    Id = 8,
                    Name = "Trip 1",
                    Stops = new List<Stop>()
                    {
                      new Stop() { Name = "Stop 1", CountryId = 5, Country = new Country{ Id = 5, Name = "Poland" } },
                      new Stop() { Name = "Stop 2", CountryId = 5, Country = new Country{ Id = 5, Name = "Poland" } }
                    }
                };

                context.Add(trip1);
                await context.SaveChangesAsync();
            }

            using (var context = new MCBContext(dbOptions))
            {
                var geoRepository = new GeoRepository(context);

                // Act
                var countries = await geoRepository.GetCountriesForTrip(8);

                // Assert
                Assert.Single(countries);
            }
        }


        private DbContextOptions<MCBContext> GetDbOptions()
        {
            var connectionStringBuilder =
                             new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<MCBContext>()
                .UseSqlite(connection)
                .Options;
            return options;
        }
    }
}