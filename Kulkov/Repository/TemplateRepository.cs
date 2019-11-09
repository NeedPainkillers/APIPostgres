using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Repository
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccount(string id);
        Task<Account> GetAccountByName(string id);
        Task AddAccount(Account item);
        void RemoveAccount(string id);
        // обновление содержания (body) записи
        void UpdateAccount(string id, Account item);
        void UpdateAccounts(string id, Account item);
    }

    public class TemplateRepository : ITemplateRepository
    {
        private readonly TemplateContext _context = null;

        public TemplateRepository(IOptions<Settings> settings)
        {
            _context = new TemplateContext(settings);
        }

        public Task AddAccount(Account item)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccount(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountByName(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAccounts()
        {
            return _context.Accounts;
        }

        public void RemoveAccount(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(string id, Account item)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccounts(string id, Account item)
        {
            throw new NotImplementedException();
        }
    }
}
