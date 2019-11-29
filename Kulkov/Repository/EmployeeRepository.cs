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
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(string id);
        Task<Employee> GetEmployeeByName(string id);
        Task AddEmployee(Employee item);
        void RemoveEmployee(string id);
        // обновление содержания (body) записи
        void UpdateEmployee(string id, Employee item);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TemplateContext _context = null;

        public EmployeeRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public async Task AddEmployee(Employee item)
        {
            //INSERT INTO public."Employees" (id_emp, first_name, last_name, patronymic, gender, hire_date, id_post) VALUES (1, 'test', 'example', 'example', false, '2019-11-29 08:13:03.661000', 1);
            var connection = _context.GetConnection;

            if(connection.State != System.Data.ConnectionState.Open)
                 await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"Employess\" (first_name, last_name, patronymic, gender, hire_date, id_post) VALUES ((@name), (@last), (@patron), (@gender), (@hire), (@post));", conn))
            {
                cmd.Parameters.AddWithValue("name", item.first_name);
                cmd.Parameters.AddWithValue("last", item.last_name);
                cmd.Parameters.AddWithValue("patron", item.patronymic);
                cmd.Parameters.AddWithValue("gender", item.gender);
                cmd.Parameters.AddWithValue("hire", item.hire_date);
                cmd.Parameters.AddWithValue("post", item.id_post);
                await cmd.ExecuteNonQueryAsync();
            }
;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            List<Employee> Response = new List<Employee>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT t.*, CTID FROM public.\"Employees\" t ORDER BY id_emp ASC", connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Employee()
                    {
                        id_emp = Int32.Parse(reader.GetValue(0).ToString()),
                        first_name = reader.GetValue(1).ToString(),
                        last_name = reader.GetValue(2).ToString(),
                        patronymic = reader.GetValue(3).ToString(),
                        gender = reader.GetBoolean(4),
                        hire_date = reader.GetDateTime(5),
                        id_post = Int32.Parse(reader.GetValue(6).ToString())
                    });
                }
            return Response;
        }

        public async Task<Employee> GetEmployee(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            int idi;
            if (!Int32.TryParse(id, idi))
                throw new Exception("id cannot be converted to integer");
            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.*, CTID FROM public.\"Employees\" t WHERE t.id_emp = {0}", idi), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Employee()
            {
                id_emp = Int32.Parse(reader.GetValue(0).ToString()),
                first_name = reader.GetValue(1).ToString(),
                last_name = reader.GetValue(2).ToString(),
                patronymic = reader.GetValue(3).ToString(),
                gender = reader.GetBoolean(4),
                hire_date = reader.GetDateTime(5),
                id_post = Int32.Parse(reader.GetValue(6).ToString())
            };
        }

        public async Task<Employee> GetEmployeeByName(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async void RemoveEmployee(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            //DELETE FROM "public"."Employees" WHERE "id_emp" = 4

            await using (var cmd = new NpgsqlCommand("DELETE FROM \"public\".\"Employees\" WHERE \"id_emp\" = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async void UpdateEmployee(string id, Employee item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Employees\" SET (first_name, last_name, patronymic, gender, hire_date, id_post) =" +
                " ((@name), (@last), (@patron), (@gender), (@hire), (@post)) WHERE id_emp = (@id);", conn))
            {
                cmd.Parameters.AddWithValue("name", item.first_name);
                cmd.Parameters.AddWithValue("last", item.last_name);
                cmd.Parameters.AddWithValue("patron", item.patronymic);
                cmd.Parameters.AddWithValue("gender", item.gender);
                cmd.Parameters.AddWithValue("hire", item.hire_date);
                cmd.Parameters.AddWithValue("post", item.id_post);
                cmd.Parameters.AddWithValue("id", Int32.Parse(id));
                await cmd.ExecuteNonQueryAsync();
            }
        }

        
    }
}
