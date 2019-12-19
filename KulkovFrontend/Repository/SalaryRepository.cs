using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetAllSalaries();
        Task<Salary> GetSalary(int id);
        Task<Salary> GetSalaryByName(int id);
        Task AddSalary(Salary item);
        Task RemoveSalary(int id);
        // обновление содержания (body) записи
        Task UpdateSalary(int id, Salary item);
        Task TransactionExample(int id1, int id2);
        Task<bool> IndexSalaries();
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

        public async Task<Salary> GetSalary(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();


            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Salaries\" t WHERE t.id_emp = {0}", id), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Salary()
            {
                id_emp = Int32.Parse(reader.GetValue(0).ToString()),
                salary = Int32.Parse(reader.GetValue(1).ToString()),
                fee = Int32.Parse(reader.GetValue(2).ToString()),
                time_update = reader.GetDateTime(3)
            };
        }

        public async Task<Salary> GetSalaryByName(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async Task<bool> IndexSalaries()
        {
            //select sal_cur();
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var cmd = new NpgsqlCommand("select sal_cur()", connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return reader.GetBoolean(0);
        }

        public async Task RemoveSalary(int id)
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

        public async Task TransactionExample(int id1, int id2)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("START TRANSACTION;" +
                "CALL example_transact((@id1)); " +
                "savepoint one; " +
                "CALL example_transact((@id2)); " +
                "rollback to one; " +
                "COMMIT;", connection))
            {
                cmd.Parameters.AddWithValue("id1", id1);
                cmd.Parameters.AddWithValue("id2", id2);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateSalary(int id, Salary item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Salaries\" SET (salary, fee) =" +
                " ((@salary), (@fee)) WHERE id_emp = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("salary", item.salary);
                cmd.Parameters.AddWithValue("fee", item.fee);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
