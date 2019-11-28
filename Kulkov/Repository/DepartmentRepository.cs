using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Kulkov.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartment(string id);
        Task<Department> GetDepartmentByName(string id);
        Task AddDepartment(Department item);
        void RemoveDepartment(string id);
        // обновление содержания (body) записи
        void UpdateDepartment(string id, Department item);
        void UpdateDepartments(string id, Department item);
    }

    public class DepartmentRepository :IDepartmentRepository
    {
        private readonly TemplateContext _context = null;

        public DepartmentRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public Task AddDepartment(Department item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetDepartment(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetDepartmentByName(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveDepartment(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(string id, Department item)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartments(string id, Department item)
        {
            throw new NotImplementedException();
        }
    }
}
