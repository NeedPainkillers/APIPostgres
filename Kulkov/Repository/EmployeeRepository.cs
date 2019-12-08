using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();        
        Task<IEnumerable<Employee>> GetAllEmployees(int mode);
        Task<Employee> GetEmployee(string id);
        Task<Employee> GetEmployee(string id, int mode);
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

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"Employess\" (first_name, last_name, patronymic, gender, hire_date, id_post) " +
                "VALUES ((@name), (@last), (@patron), (@gender), (@hire), (@post));", connection))
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
            await using (var cmd = new NpgsqlCommand("SELECT t.* FROM public.\"Employees\" t ORDER BY id_emp ASC", connection))
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

        public Task<IEnumerable<Employee>> GetAllEmployees(int mode)
        {
            return GetAllEmployeesInternal(mode);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesInternal(int mode)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            switch (mode)
            {
                case 1:
                    return await GetEmployeesFull();
                case 2:
                    return await GetEmployeesSalary();
                case 3:
                    return await GetEmployeesDepartment();
                default:
                    throw new NotImplementedException();
            }
        }

        private async Task<IEnumerable<Employee>> GetEmployeesFull()
        {
            var connection = _context.GetConnection;
            //TODO:
            List<Employee> Response = new List<Employee>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT t.* FROM public.\"Employees\" t ORDER BY id_emp ASC", connection))
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
        
        private async Task<IEnumerable<Employee>> GetEmployeesSalary()
        {
            var connection = _context.GetConnection;
            List<Employee> Response = new List<Employee>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT t.* FROM public.\"Employees\" t ORDER BY id_emp ASC", connection))
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
        private async Task<IEnumerable<Employee>> GetEmployeesDepartment()
        {
            var connection = _context.GetConnection;
            List<Employee> Response = new List<Employee>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT t.* FROM public.\"Employees\" t ORDER BY id_emp ASC", connection))
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

            if (!Int32.TryParse(id, out int idi))
                throw new Exception("id cannot be converted to integer");

            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Employees\" t WHERE t.id_emp = {0}", idi), connection);
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
                " ((@name), (@last), (@patron), (@gender), (@hire), (@post)) WHERE id_emp = (@id);", connection))
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

        public async Task<Employee> GetEmployee(string id, int mode)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            switch (mode)
            {
                case 1:
                    return await GetEmployeeFull(id, mode);
                case 2:
                    return await GetEmployeeSalary(id, mode);
                case 3:
                    return await GetEmployeeDepartment(id, mode);
                default:
                    throw new NotImplementedException();
            }
        }

        private async Task<Employee> GetEmployeeDepartment(string id, int mode)
        {
            throw new NotImplementedException();
        }

        private async Task<Employee> GetEmployeeSalary(string id, int mode)
        {
            throw new NotImplementedException();
        }

        private async Task<Employee> GetEmployeeFull(string id, int mode)
        {
            throw new NotImplementedException();
        }
    }
}
