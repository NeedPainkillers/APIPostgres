using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulkov.Data;
using Kulkov.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kulkov.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController()
        { 

        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            var template = new Template("Temp");
            await template.TestConnection();
            return await template.Accounts;
        }
    }
}
