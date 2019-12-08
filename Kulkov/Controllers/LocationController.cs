using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulkov.Repository;
using Kulkov.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kulkov.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locRepository;

        public LocationController(ILocationRepository locRepository)
        {
            _locRepository = locRepository;
        }

        // GET api/Location
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public Task<IEnumerable<Location>> Get()
        {
            return GetLocationsInternal();
        }

        private async Task<IEnumerable<Location>> GetLocationsInternal()
        {

            return await _locRepository.GetAllLocations();
        }

        // GET api/Location/5
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("{id}")]
        public Task<Location> Get(string id)
        {
            return GetLocationIdInternal(id);

        }

        private async Task<Location> GetLocationIdInternal(string id)
        {
            return await _locRepository.GetLocation(id) ?? new Location();
        }


        // POST api/Location
        [HttpPost]
        public async Task<bool> Post([FromBody] Location item)
        {
            await _locRepository.AddLocation(item);

            return true;
        }

        // PUT api/Location/5
        [HttpPut("{id}")]
        public bool Put(string id, [FromBody] Location item)
        {
            _locRepository.UpdateLocation(id, item);

            return true;
        }

        // DELETE api/Location/23243423
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            _locRepository.RemoveLocation(id);

            return true;
        }
    }
}