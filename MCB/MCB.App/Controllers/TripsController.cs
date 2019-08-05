using AutoMapper;
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

        [HttpGet("{id}", Name = "GetTrip")]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.mcb.trip+json" })]
        public async Task<IActionResult> GetTrip(int id)
        {
            return await GetSpecificTrip<TripModel>(id);
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
