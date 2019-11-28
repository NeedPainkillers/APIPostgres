using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
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

        public Task AddSalary(Salary item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Salary>> GetAllSalaries()
        {
            throw new NotImplementedException();
        }

        public Task<Salary> GetSalary(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Salary> GetSalaryByName(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveSalary(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateSalaries(string id, Salary item)
        {
            throw new NotImplementedException();
        }

        public void UpdateSalary(string id, Salary item)
        {
            throw new NotImplementedException();
        }
    }
}
