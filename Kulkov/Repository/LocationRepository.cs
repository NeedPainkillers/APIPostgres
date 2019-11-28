using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location> GetLocation(string id);
        Task<Location> GetLocationByDepartment(string id);
        Task AddLocation(Location item);
        void RemoveLocation(string id);
        // обновление содержания (body) записи
        void UpdateLocation(string id, Location item);
        void UpdateLocations(string id, Location item);
    }

    public class LocationRepository : ILocationRepository
    {
        private readonly TemplateContext _context = null;

        public LocationRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public Task AddLocation(Location item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Location>> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public Task<Location> GetLocation(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Location> GetLocationByDepartment(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveLocation(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocation(string id, Location item)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocations(string id, Location item)
        {
            throw new NotImplementedException();
        }
    }
}
