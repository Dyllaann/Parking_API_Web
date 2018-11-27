using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParKing.Utils.Configuration;

namespace ParKing.Application.MobileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController
    {
        public TestController(Config config)
        {
            Config = config;
        }

        private Config Config { get; }

        [HttpGet]
        public string Test()
        {
            return Config.DatabaseConnectionString;
        }
    }
}
