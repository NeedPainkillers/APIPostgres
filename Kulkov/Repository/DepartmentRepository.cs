using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;


namespace Kulkov.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<IEnumerable<Department>> GetDepartmentsHaving();
        Task<Department> GetDepartment(string id);
        Task<IEnumerable<Department>> GetDepartmentByName(string id);
        Task AddDepartment(Department item);
        void RemoveDepartment(string id);
        // обновление содержания (body) записи
        void UpdateDepartment(string id, Department item);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly TemplateContext _context = null;

        public DepartmentRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }


        public async Task AddDepartment(Department item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"Departments\" (dept_name, id_head, id_loc) " +
                "VALUES ((@name), (@head), (@loc));", connection))
            {
                cmd.Parameters.AddWithValue("name", item.dept_name);
                cmd.Parameters.AddWithValue("head", item.id_head);
                cmd.Parameters.AddWithValue("loc", item.id_loc);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            List<Department> Response = new List<Department>();
            await using (var cmd = new NpgsqlCommand("SELECT t.*, CTID FROM public.\"Departments\" t ORDER BY id_dept ASC", connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Department()
                    {
                        id_dept = Int32.Parse(reader.GetValue(0).ToString()),
                        dept_name = reader.GetValue(1).ToString(),
                        id_loc = Int32.Parse(reader.GetValue(2).ToString()),
                        id_head = Int32.Parse(reader.GetValue(3).ToString())
                    });
                }
            return Response;
        }

        public async Task<Department> GetDepartment(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            if (!Int32.TryParse(id, out int idi))
                throw new Exception("id cannot be converted to integer");

            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Departments\" t WHERE t.id_dept = {0}", idi), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Department()
            {
                id_dept = Int32.Parse(reader.GetValue(0).ToString()),
                dept_name = reader.GetValue(1).ToString(),
                id_loc = Int32.Parse(reader.GetValue(2).ToString()),
                id_head = Int32.Parse(reader.GetValue(3).ToString())
            };
        }

        public async Task<IEnumerable<Department>> GetDepartmentByName(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            //TODOL: check input for prohibited symbols

            List<Department> Response = new List<Department>();
            await using (var cmd = new NpgsqlCommand("SELECT t.* FROM public.\"Departments\" t WHERE t.dept_name = (@name) ORDER BY id_dept ASC", connection))
            {
                cmd.Parameters.AddWithValue("name", id);
                await using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Response.Add(new Department()
                        {
                            id_dept = Int32.Parse(reader.GetValue(0).ToString()),
                            dept_name = reader.GetValue(1).ToString(),
                            id_loc = Int32.Parse(reader.GetValue(2).ToString()),
                            id_head = Int32.Parse(reader.GetValue(3).ToString())
                        });
                    }
            }
            return Response;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsHaving()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            List<Department> Response = new List<Department>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT d.id_dept, SUM(s.salary) FROM public.\"Employees\" e " +
                                                    "LEFT JOIN public.\"Salaries\" s ON s.id_emp = e.id_emp " +
                                                    "LEFT JOIN public.\"dept_empl\" d ON d.id_emp = e.id_emp " +
                                                    "GROUP BY d.id_dept" +
                                                    "HAVING SUM(s.salary) > 5000;"
                                                    , connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Department()
                    {
                        id_dept = reader.GetInt32(0),
                        additionalInfo = JsonSerializer.Serialize(new { Salaries = reader.GetValue(1).ToString() })
                    });
                }
            return Response;

        }

        public async void RemoveDepartment(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("DELETE FROM \"public\".\"Departments\" WHERE \"id_dept\" = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async void UpdateDepartment(string id, Department item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Departments\" SET (dept_name, id_head, id_loc) =" +
                " ((@name), (@head), (@lock)) WHERE id_dept = (@id);", connection))
            {
                cmd.Parameters.AddWithValue("name", item.dept_name);
                cmd.Parameters.AddWithValue("head", item.id_head);
                cmd.Parameters.AddWithValue("loc", item.id_loc);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
