using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParKing.Business.Services;
using ParKing.Data.Engine;
using ParKing.Utils.Configuration;
using Serilog;

namespace ParKing.Application.MobileApi.Controllers
{
    [Route("api/availability")]
    public class AvailabilityController : Controller
    {
        public Config Configuration{get; set; }
        public AvailabilityService AvailabilityService { get;set; }

        public AvailabilityController(Config configuration, AvailabilityService availabilityService)
        {
            Configuration = configuration;
            AvailabilityService = availabilityService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAvailabilities()
        {
            Log.Logger.Information($"Starting {MethodBase.GetCurrentMethod().Name}");

            var locations = AvailabilityService.GetAvailabilities();
            return locations.Count > 0
                ? Ok(locations)
                : (IActionResult) BadRequest();
        }
    }
}
