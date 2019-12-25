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
        Task<Department> GetDepartment(int id);
        Task<IEnumerable<Department>> GetDepartmentByName(int id);
        Task<IEnumerable<Department>> GetDepartmentAny();
        Task AddDepartment(Department item);
        Task<IEnumerable<Employee>> GetEmployees (int id_dept);
        Task AddEmployee(int id_dep, int id_emp);
        Task DeleteEmployee(int id_dept, int id_emp);
        Task RemoveDepartment(int id);
        // обновление содержания (body) записи
        Task UpdateDepartment(int id, Department item);
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
                string.Format("VALUES ('{0}', (@head), (@loc));", item.dept_name), connection))
            {
                cmd.Parameters.AddWithValue("head", item.id_head);
                cmd.Parameters.AddWithValue("loc", item.id_loc);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task AddEmployee(int id_dept, int id_emp)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"dept_empl\" ( id_emp, id_dept) " +
                "VALUES ((@id_emp), (@id_dept));", connection))
            {
                cmd.Parameters.AddWithValue("id_emp",   id_emp);
                cmd.Parameters.AddWithValue("id_dept",  id_dept);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteEmployee(int id_dept, int id_emp)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("DELETE FROM \"public\".\"dept_empl\" WHERE id_emp = (@id_emp) and id_dept = (@id_dept);", connection))
            {
                cmd.Parameters.AddWithValue("id_emp", id_emp);
                cmd.Parameters.AddWithValue("id_dept", id_dept);
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

        public async Task<Department> GetDepartment(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();


            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Departments\" t WHERE t.id_dept = {0}", id), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            return new Department()
            {
                id_dept = Int32.Parse(reader.GetValue(0).ToString()),
                dept_name = reader.GetValue(1).ToString(),
                id_loc = Int32.Parse(reader.GetValue(2).ToString()),
                id_head = Int32.Parse(reader.GetValue(3).ToString())
            };
        }

        public async Task<IEnumerable<Department>> GetDepartmentAny()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            /*
             *  SELECT d.dept_name, AVG(s.salary)
                FROM public."dept_empl" de
                LEFT JOIN "Employees" e ON e.id_emp = de.id_emp
                LEFT JOIN "Salaries" s ON s.id_emp = e.id_emp
                LEFT JOIN "Departments" d ON d.id_dept = de.id_dept
                GROUP BY de.id_dept, d.dept_name
                HAVING AVG(s.salary)>=ALL
                (SELECT AVG(s1.salary) FROM public."dept_empl" d1
                LEFT JOIN "Employees" e1 ON e1.id_emp = d1.id_emp
                LEFT JOIN "Salaries" s1 ON s1.id_emp = e1.id_emp
                GROUP BY d1.id_dept);
                */
            List<Department> Response = new List<Department>();
            await using (var cmd = new NpgsqlCommand("SELECT d.dept_name, AVG(s.salary) FROM public.\"dept_empl\" de LEFT JOIN \"Employees\" e ON e.id_emp = de.id_emp LEFT JOIN \"Salaries\" s ON s.id_emp = e.id_emp LEFT JOIN \"Departments\" d ON d.id_dept = de.id_dept GROUP BY de.id_dept, d.dept_name HAVING AVG(s.salary)>=ALL (SELECT AVG(s1.salary) FROM public.\"dept_empl\" d1 LEFT JOIN \"Employees\" e1 ON e1.id_emp = d1.id_emp LEFT JOIN \"Salaries\" s1 ON s1.id_emp = e1.id_emp GROUP BY d1.id_dept);", connection))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Response.Add(new Department()
                        {
                            dept_name = reader.GetValue(0).ToString(),
                            additionalInfo = reader.GetValue(1).ToString()
                        });
                    }
            }
            return Response;
        }

        public async Task<IEnumerable<Department>> GetDepartmentByName(int id)
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
            await using (var cmd = new NpgsqlCommand("SELECT d.id_dept, d1.dept_name, SUM(s.salary) FROM public.\"Employees\" e " +
                                                    "LEFT JOIN public.\"Salaries\" s ON s.id_emp = e.id_emp " +
                                                    "LEFT JOIN public.\"dept_empl\" d ON d.id_emp = e.id_emp " +
                                                    "LEFT JOIN public.\"Departments\" d1 ON d1.id_dept = d.id_dept " +
                                                    "GROUP BY d.id_dept, d1.id_dept " +
                                                    "HAVING SUM(s.salary) > 5000;"
                                                    , connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Department()
                    {
                        id_dept = reader.GetInt32(0),
                        dept_name = reader.GetString(1),
                        additionalInfo = reader.GetValue(2).ToString()
                    });
                }
            return Response;

        }

        public async Task<IEnumerable<Employee>> GetEmployees(int id_dept)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            List<Employee> Response = new List<Employee>();
            await using (var cmd = new NpgsqlCommand("SELECT e.* FROM public.dept_empl t " +
                "join public.\"Employees\" e on e.id_emp = t.id_emp " +
                "where t.id_dept = (@id_dept);", connection))
            {
                cmd.Parameters.AddWithValue("id_dept", id_dept);
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
            }
            return Response;
        }

        public async Task RemoveDepartment(int id)
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

        public async Task UpdateDepartment(int id, Department item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Departments\" SET (dept_name, id_head, id_loc) =" +
                string.Format(" ('{0}', (@head), (@loc)) WHERE id_dept = (@id);", item.dept_name), connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("head", item.id_head);
                cmd.Parameters.AddWithValue("loc", item.id_loc);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
