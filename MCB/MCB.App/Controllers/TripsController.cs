﻿using AutoMapper;
using MCB.Business.CoreHelper.Attributes;
using MCB.Business.Models.Trips;
using MCB.Data.RepositoriesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace MCB.App.Controllers
{
    [Route("api/trips/")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public TripsController(ITripRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Get an Trip by id
        /// </summary>
        /// <param name="id">Id of the Trip</param>
        /// <returns>An Trip based on the MediaType</returns>
        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.mcb.trip+json" })]
        public async Task<IActionResult> GetTrip(int id)
        {
            return await GetSpecificTrip<TripModel>(id);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.mcb.tripwithstops+json" })]
        public async Task<IActionResult> GetTripWithStops(int id)
        {
            return await GetSpecificTrip<TripWithStopsModel>(id, true);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.mcb.tripwithstopsandusers+json" })]
        public async Task<IActionResult> GetTripWithStopsAndUsers(int id)
        {
            return await GetSpecificTrip<TripWithStopsAndUsersModel>(id, true, true);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.mcb.tripwithcountries+json" })]
        public async Task<IActionResult> GetTripWithCountries(int id)
        {
            return await GetSpecificTrip<TripWithCountriesModel>(id, true);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.mcb.tripwithcountriesandstats+json" })]
        public async Task<IActionResult> GetTripWithCountriesAndStats(int id)
        {
            return await GetSpecificTrip<TripWithCountriesAndStatsModel>(id, true, true);
        }

        [HttpGet("{id}")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.mcb.tripwithcountriesandworldheritages+json" })]
        public async Task<IActionResult> GetTripWithCountriesAndWorldHeritages(int id)
        {
            return await GetSpecificTrip<TripWithCountriesAndWorldHeritagesModel>(id, true, true);
        }

        private async Task<IActionResult> GetSpecificTrip<T>(int tripId, bool includeStops = false, bool includeUsers = false) where T : class
        {
            var tripFromRepo = await _repository.GetTrip(tripId, includeStops, includeUsers);

            if (tripFromRepo == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<T>(tripFromRepo));
        }
    }
}
