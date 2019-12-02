using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetAllSalaries();
        Task<Salary> GetSalary(string id);
        Task<Salary> GetSalaryByName(string id);
        Task AddSalary(Salary item);
        void RemoveSalary(string id);
        // обновление содержания (body) записи
        void UpdateSalary(string id, Salary item);
        void UpdateSalaries(string id, Salary item);
    }

    public class SalaryRepository : ISalaryRepository
    {
        private readonly TemplateContext _context = null;

        public SalaryRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public async Task AddSalary(Salary item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"Salaries\" (id_emp, salary, fee) " +
                "VALUES ((@id), (@salary), (@fee));", connection))
            {
                cmd.Parameters.AddWithValue("id", item.id_emp);
                cmd.Parameters.AddWithValue("salary", item.salary);
                cmd.Parameters.AddWithValue("fee", item.fee);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Salary>> GetAllSalaries()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            List<Salary> Response = new List<Salary>();
            await using (var cmd = new NpgsqlCommand("SELECT t.* FROM public.\"Salaries\" t ORDER BY id_emp ASC", connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Salary()
                    {
                        id_emp = Int32.Parse(reader.GetValue(0).ToString()),
                        salary = Int32.Parse(reader.GetValue(1).ToString()),
                        fee = Int32.Parse(reader.GetValue(2).ToString()),
                        time_update = reader.GetDateTime(3)
                    });
                }
            return Response;
        }

        public async Task<Salary> GetSalary(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            if (!Int32.TryParse(id, out int idi))
                throw new Exception("id cannot be converted to integer");

            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Salaries\" t WHERE t.id_emp = {0}", idi), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Salary()
            {
                id_emp = Int32.Parse(reader.GetValue(0).ToString()),
                salary = Int32.Parse(reader.GetValue(1).ToString()),
                fee = Int32.Parse(reader.GetValue(2).ToString()),
                time_update = reader.GetDateTime(3)
            };
        }

        public async Task<Salary> GetSalaryByName(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async void RemoveSalary(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("DELETE FROM \"public\".\"Salaries\" WHERE \"id_emp\" = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async void UpdateSalaries(string id, Salary item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async void UpdateSalary(string id, Salary item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Salaries\" SET (salary, fee) =" +
                " ((@address), (@city)) WHERE id_dept = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", item.id_emp);
                cmd.Parameters.AddWithValue("address", item.salary);
                cmd.Parameters.AddWithValue("city", item.fee);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
