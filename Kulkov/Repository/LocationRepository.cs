using Kulkov.Data;
using Kulkov.UOW;
using Npgsql;
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
    }

    public class LocationRepository : ILocationRepository
    {
        private readonly TemplateContext _context = null;

        public LocationRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public async Task AddLocation(Location item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"Locations\" (address, city) " +
                "VALUES ((@address), (@city));", connection))
            {
                cmd.Parameters.AddWithValue("address", item.address);
                cmd.Parameters.AddWithValue("city", item.city);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            List<Location> Response = new List<Location>();
            await using (var cmd = new NpgsqlCommand("SELECT t.*, CTID FROM public.\"Locations\" t ORDER BY id_loc ASC", connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Location()
                    {
                        id_loc = Int32.Parse(reader.GetValue(0).ToString()),
                        address = reader.GetValue(1).ToString(),
                        city = reader.GetValue(2).ToString()
                    });
                }
            return Response;
        }

        public async Task<Location> GetLocation(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            if (!Int32.TryParse(id, out int idi))
                throw new Exception("id cannot be converted to integer");

            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Locations\" t WHERE t.id_loc = {0}", idi), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Location()
            {
                id_loc = Int32.Parse(reader.GetValue(0).ToString()),
                address = reader.GetValue(1).ToString(),
                city = reader.GetValue(2).ToString()
            };
        }

        public async Task<Location> GetLocationByDepartment(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async void RemoveLocation(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("DELETE FROM \"public\".\"Locations\" WHERE \"id_loc\" = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async void UpdateLocation(string id, Location item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Departments\" SET (address, city) =" +
                " ((@address), (@city)) WHERE id_dept = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", item.id_loc);
                cmd.Parameters.AddWithValue("address", item.address);
                cmd.Parameters.AddWithValue("city", item.city);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
