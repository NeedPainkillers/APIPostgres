using Kulkov.Data;
using Kulkov.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kulkov.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Handles Employee entity
        /// Get request can operate in following modes as query parameter
        ///     1 - joins employee table with salary and department
        ///     2 - joins employee with salary
        ///     3 - joins employee with department
        /// Example: GET api/employee?mode=1
        /// 
        /// For POST PUT DELETE methods use corresponding controllers for each entity
        /// </summary>

        private readonly IEmployeeRepository _empRepository;


        public EmployeeController(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }

        // GET api/employee
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public Task<IEnumerable<Employee>> Get([FromQuery] int mode, [FromQuery] bool order = true)
        {
            switch (mode)
            {
                case 1:
                    return GetFullEmployeesInternal();
                case 2:
                    return GetSalaryEmployeesInternal(order);
                case 3:
                    return GetDepartmentEmployeesInternal();
                default:
                    return GetEmployeesInternal();
            }
        }

        private async Task<IEnumerable<Employee>> GetEmployeesInternal()
        {

            return await _empRepository.GetAllEmployees();
        }

        private async Task<IEnumerable<Employee>> GetFullEmployeesInternal()
        {

            return await _empRepository.GetAllEmployees(1);
        }

        private async Task<IEnumerable<Employee>> GetSalaryEmployeesInternal(bool order)
        {

            return await _empRepository.GetAllEmployees(2, order);
        }

        private async Task<IEnumerable<Employee>> GetDepartmentEmployeesInternal()
        {

            return await _empRepository.GetAllEmployees(3);
        }

        // GET api/Employee/5
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("{id}")]
        public Task<Employee> Get(string id, [FromQuery] int mode)
        {
            switch (mode)
            {
                case 1:
                    return GetFullEmployeeIdInternal(id);
                case 2:
                    return GetSalaryEmployeeIdInternal(id);
                case 3:
                    return GetDepartmentEmployeeIdInternal(id);
                default:
                    return GetEmployeeIdInternal(id);
            }
        }

        private async Task<Employee> GetEmployeeIdInternal(string id)
        {
            return await _empRepository.GetEmployee(id) ?? new Employee();
        }
        private async Task<Employee> GetSalaryEmployeeIdInternal(string id)
        {
            return await _empRepository.GetEmployeeCase(id) ?? new Employee();
        }
        private async Task<Employee> GetDepartmentEmployeeIdInternal(string id)
        {
            return await _empRepository.GetEmployee(id) ?? new Employee();
        }
        private async Task<Employee> GetFullEmployeeIdInternal(string id)
        {
            return await _empRepository.GetEmployee(id) ?? new Employee();
        }


        // POST api/Employee
        [HttpPost]
        public async Task<bool> Post([FromBody] Employee item)
        {
            await _empRepository.AddEmployee(item);

            return true;
        }

        // PUT api/Employee/5
        [HttpPut("{id}")]
        public bool Put(string id, [FromBody] Employee item)
        {
            _empRepository.UpdateEmployee(id, item);

            return true;
        }

        // DELETE api/team/23243423
        [HttpDelete("{id}")]
        public bool Delete(string id, [FromBody] string senderId)
        {
            _empRepository.RemoveEmployee(id);

            return true;
        }


    }
}