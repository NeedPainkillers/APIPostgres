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

        public async Task AddSalary(Salary item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Salary>> GetAllSalaries()
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async Task<Salary> GetSalary(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async Task<Salary> GetSalaryByName(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async void RemoveSalary(string id)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async void UpdateSalaries(string id, Salary item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }

        public async void UpdateSalary(string id, Salary item)
        {
            var connection = _context.GetConnection;

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();
            throw new NotImplementedException();
        }
    }
}
