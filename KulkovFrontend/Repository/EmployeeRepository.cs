using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Kulkov.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetAllEmployees(int mode, bool order = true);
        Task<IEnumerable<Employee>> GetEmployeesCorr();
        Task<Employee> GetEmployee(int id);
        Task<Employee> GetEmployee(int id, int mode);
        Task<Employee> GetEmployeeCase(int id);
        Task<Employee> GetEmployeeByName(int id);

        Task AddEmployee(Employee item);
        Task RemoveEmployee(int id);
        // обновление содержания (body) записи
        Task UpdateEmployee(int id, Employee item);
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

            await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.\"Employees\" (first_name, last_name, patronymic, gender, hire_date, id_post) " +
                String.Format("VALUES ('{0}', '{1}', '{2}', (@gender), (@hire), (@post));", item.first_name, item.last_name, item.patronymic), connection))
            {
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

        public Task<IEnumerable<Employee>> GetAllEmployees(int mode, bool order = true)
        {
            return GetAllEmployeesInternal(mode, order);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesInternal(int mode, bool order = true)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            switch (mode)
            {
                case 1:
                    return await GetEmployeesFull();
                case 2:
                    return await GetEmployeesSalary(order);
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

        private async Task<IEnumerable<Employee>> GetEmployeesSalary(bool order)
        ///
        /// View example
        ///
        {

            var connection = _context.GetConnection;
            List<Employee> Response = new List<Employee>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand(String.Format("select * from view_order({0});", order), connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Employee()
                    {
                        first_name = reader.GetValue(0).ToString(),
                        last_name = reader.GetValue(1).ToString(),
                        patronymic = reader.GetValue(2).ToString(),
                        salary = new Salary() { salary = reader.GetInt32(3), time_update = reader.GetDateTime(4) },
                    });
                }
            return Response;
        }

        private async Task<IEnumerable<Employee>> GetEmployeesDepartment()
        {
            //TBD
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

        public async Task<Employee> GetEmployee(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();


            await using var cmd = new NpgsqlCommand(String.Format("SELECT t.* FROM public.\"Employees\" t WHERE t.id_emp = {0}", id), connection);
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

        public async Task<Employee> GetEmployeeByName(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async Task RemoveEmployee(int id)
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

        public async Task UpdateEmployee(int id, Employee item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE taskdb.public.\"Employees\" SET (first_name, last_name, patronymic, gender, hire_date, id_post) =" +
                String.Format(" ('{0}', '{1}', '{2}', (@gender), (@hire), (@post)) WHERE id_emp = (@id);", item.first_name, item.last_name, item.patronymic), connection))
            {
                cmd.Parameters.AddWithValue("gender", item.gender);
                cmd.Parameters.AddWithValue("hire", item.hire_date);
                cmd.Parameters.AddWithValue("post", item.id_post);
                cmd.Parameters.AddWithValue("id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<Employee> GetEmployee(int id, int mode)
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

        private async Task<Employee> GetEmployeeDepartment(int id, int mode)
        {
            throw new NotImplementedException();
        }

        private async Task<Employee> GetEmployeeSalary(int id, int mode)
        {
            return await GetEmployeeCase(id);
        }

        private async Task<Employee> GetEmployeeFull(int id, int mode)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployeeCase(int id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var cmd = new NpgsqlCommand(String.Format("SELECT e.last_name, " +
                                                                    "e.first_name, " +
                                                                    "e.patronymic, " +
                                                                    "s.salary, " +
                                                                    "CASE WHEN s.salary = 0 or s.salary is null THEN 'not given yet' " +
                                                                    "ELSE CAST(calculate_salary(s.salary, s.fee) as VARCHAR(20)) " +
                                                                    "END AS salary_monthly " +
                                                                    "FROM  public.\"Employees\" e " +
                                                                    "LEFT JOIN public.\"Salaries\" s ON s.id_emp = e.id_emp" +
                                                                    "WHERE e.id_emp = {0};", id), connection);
            await using var reader = await cmd.ExecuteReaderAsync();
            if (!Int32.TryParse(reader.GetString(4), out int fee))
                fee = -1;
            return new Employee()
            {
                first_name = reader.GetValue(0).ToString(),
                last_name = reader.GetValue(1).ToString(),
                patronymic = reader.GetValue(2).ToString(),
                salary = new Salary() { salary = reader.GetInt32(3), fee = fee }
            };
        }

        public async Task<IEnumerable<Employee>> GetEmployeesCorr()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            List<Employee> Response = new List<Employee>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT v.last_name, v.salary, (SELECT AVG(s.salary) FROM public.\"Salaries\" s) " +
                                                    "FROM emp_sal_join() v " +
                                                    "WHERE v.salary >= " +
                                                    "(SELECT AVG(e.salary) FROM(SELECT * FROM public.\"Employees\"  LEFT JOIN public.\"Salaries\" s ON s.id_emp = v.id_emp)e);"
                                                    , connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Employee()
                    {
                        last_name = reader.GetString(0),
                        salary = new Salary() { salary = reader.GetInt32(1) },
                        additionalInfo = JsonSerializer.Serialize(new { avgSalary = reader.GetValue(2).ToString() })
                    });
                }
            return Response;

        }

        //public async Task<IEnumerable<Employee>> GetEmployeesHaving()
        //{
        //    /*
        //    SELECT e.id_dept, SUM(s.salary) FROM 'Employees' e GROUP BY e.id_dept HAVING SUM(s.salary) > 5000 LEFT JOIN 'Salaries' s ON s.id_emp = e.id_emp LEFT JOIN 'dept_empl' d ON d.id_emp = e.id_emp;
        //    */
        //    var connection = _context.GetConnection;

        //    if (connection.State != System.Data.ConnectionState.Open)
        //        await connection.OpenAsync();

        //    List<Employee> Response = new List<Employee>();
        //    // Retrieve all rows
        //    await using (var cmd = new NpgsqlCommand("SELECT d.id_dept, SUM(s.salary) FROM public.\"Employees\" e " +
        //                                            "LEFT JOIN public.\"Salaries\" s ON s.id_emp = e.id_emp " +
        //                                            "LEFT JOIN public.\"dept_empl\" d ON d.id_emp = e.id_emp " +
        //                                            "GROUP BY d.id_dept" +
        //                                            "HAVING SUM(s.salary) > 5000;"
        //                                            , connection))
        //    await using (var reader = await cmd.ExecuteReaderAsync())
        //        while (await reader.ReadAsync())
        //        {
        //            Response.Add(new Employee()
        //            {
        //                last_name = reader.GetString(0),
        //                salary = new Salary() { salary = reader.GetInt32(1) },
        //                additionalInfo = JsonSerializer.Serialize(new { avgSalary = reader.GetValue(2).ToString() })
        //            });
        //        }
        //    return Response;
        //}
    }
}
