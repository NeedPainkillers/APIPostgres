using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
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
        void UpdateEmployees(string id, Employee item);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TemplateContext _context = null;

        public EmployeeRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }
        public Task AddEmployee(Employee item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByName(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(string id, Employee item)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployees(string id, Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
