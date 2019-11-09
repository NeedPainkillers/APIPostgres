using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulkov.Data;
using Kulkov.Repository;
using Kulkov.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kulkov.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;

        public TemplateController(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await _templateRepository.GetAllAccounts();
        }
    }
}
