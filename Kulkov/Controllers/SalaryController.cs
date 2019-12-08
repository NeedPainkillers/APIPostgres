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
    public class SalaryController : ControllerBase
    {
        /// <summary>
        /// Handles Salary entity
        /// 
        /// For combining salariy entity with employees use Employee controller
        /// </summary>

        private readonly ISalaryRepository _salRepository;

        public SalaryController(ISalaryRepository salRepository)
        {
            _salRepository = salRepository;
        }

        // GET api/Salary
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public Task<IEnumerable<Salary>> Get()
        {
            return GetSalariesInternal();
        }

        private async Task<IEnumerable<Salary>> GetSalariesInternal()
        {

            return await _salRepository.GetAllSalaries();
        }

        // GET api/Salary/5
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("{id}")]
        public Task<Salary> Get(string id)
        {
            return GetSalaryIdInternal(id);

        }

        private async Task<Salary> GetSalaryIdInternal(string id)
        {
            return await _salRepository.GetSalary(id) ?? new Salary();
        }


        // POST api/Salary
        [HttpPost]
        public async Task<bool> Post([FromBody] Salary item) // return result 
        {
            await _salRepository.AddSalary(item);

            return true;
        }

        // PUT api/Salary/5
        [HttpPut("{id}")]
        public bool Put(string id, [FromBody] Salary item)
        {
            _salRepository.UpdateSalary(id, item);

            return true;
        }

        // DELETE api/Salary/23243423
        [HttpDelete("{id}")]
        public bool Delete(string id, [FromBody] string senderId)
        {
            _salRepository.RemoveSalary(id);

            return true;
        }



    }
}