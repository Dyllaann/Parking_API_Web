using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParKing.Business.Services;
using ParKing.Utils.Configuration;

namespace ParKing.Application.RaspberryApi.Controllers
{
    [Route("api/admin")]
    public class AdminController : Controller
    {
        public Config Configuration{get; set; }
        public AvailabilityService AvailabilityService { get;set; }

        public AdminController(Config configuration, AvailabilityService availabilityService)
        {
            Configuration = configuration;
            AvailabilityService = availabilityService;
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddLot(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            AvailabilityService.AddLot(id);
            return Ok();
        }


    }
}
