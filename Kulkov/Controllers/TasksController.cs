using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulkov.Data;
using Kulkov.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kulkov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ISalaryRepository _salRepository;

        public TasksController(ISalaryRepository salRepository)
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
        public void Post([FromQuery] int id1, [FromQuery] int id2) // return result 
        {
            _salRepository.TransactionExample(id1, id2);
        }

        // PUT api/Salary
        [HttpPut]
        public async Task<bool> Put()
        {

            return await _salRepository.IndexSalaries();
        }

        // DELETE api/Salary/23243423
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return true;
        }
    }
}