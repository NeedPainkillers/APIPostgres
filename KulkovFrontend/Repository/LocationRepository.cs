using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location> GetLocation(int id);
        Task<Location> GetLocationByDepartment(int id);
        Task AddLocation(Location item);
        Task RemoveLocation(int id);
        // обновление содержания (body) записи
        Task UpdateLocation(int id, Location item);
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

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"Location\" (address, city) " +
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
            await using (var cmd = new NpgsqlCommand("SELECT t.*, CTID FROM public.\"Location\" t ORDER BY id_loc ASC", connection))
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

        public async Task<Location> GetLocation(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Location\" t WHERE t.id_loc = {0}", id), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Location()
            {
                id_loc = Int32.Parse(reader.GetValue(0).ToString()),
                address = reader.GetValue(1).ToString(),
                city = reader.GetValue(2).ToString()
            };
        }

        public async Task<Location> GetLocationByDepartment(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async Task RemoveLocation(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("DELETE FROM \"public\".\"Location\" WHERE \"id_loc\" = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateLocation(int id, Location item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Location\" SET (address, city) =" +
                " ((@address), (@city)) WHERE id_loc = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", item.id_loc);
                cmd.Parameters.AddWithValue("address", item.address);
                cmd.Parameters.AddWithValue("city", item.city);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
