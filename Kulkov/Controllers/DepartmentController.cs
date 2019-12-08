using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulkov.Repository;
using Kulkov.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kulkov.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _depRepository;

        public DepartmentController(IDepartmentRepository depRepository)
        {
            _depRepository = depRepository;
        }

        // GET api/Department
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public Task<IEnumerable<Department>> Get()
        {
            return GetDepartmentsInternal();
        }

        private async Task<IEnumerable<Department>> GetDepartmentsInternal()
        {

            return await _depRepository.GetAllDepartments();
        }

        // GET api/Department/5
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("{id}")]
        public Task<Department> Get(string id)
        {
            return GetDepartmentIdInternal(id);

        }

        private async Task<Department> GetDepartmentIdInternal(string id)
        {
            return await _depRepository.GetDepartment(id) ?? new Department();
        }


        // POST api/Department
        [HttpPost]
        public async Task<bool> Post([FromBody] Department item)
        {
            await _depRepository.AddDepartment(item);

            return true;
        }

        // PUT api/Department/5
        [HttpPut("{id}")]
        public bool Put(string id, [FromBody] Department item)
        {
            _depRepository.UpdateDepartment(id, item);

            return true;
        }

        // DELETE api/Department/23243423
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            _depRepository.RemoveDepartment(id);

            return true;
        }
    }
}