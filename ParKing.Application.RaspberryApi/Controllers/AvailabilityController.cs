﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParKing.Business.Services;
using ParKing.Data.Engine;
using ParKing.Utils.Configuration;

namespace ParKing.Application.RaspberryApi.Controllers
{
    public class AvailabilityController : Controller
    {
        public Config Configuration{get; set; }
        public AvailabilityService AvailabilityService { get;set; }

        public AvailabilityController(Config configuration, AvailabilityService availabilityService)
        {
            Configuration = configuration;
            AvailabilityService = availabilityService;
        }

        [HttpPatch]
        [Route("")]
        public IActionResult UpdateSingleAvailability(UpdateAvailability update)
        {
            if (update == null) return BadRequest();
            
            var success = AvailabilityService.UpdateSingleAvailability(update);
            return success
                ? Ok()
                : (IActionResult) BadRequest();
        }

        [HttpPatch]
        [Route("")]
        public IActionResult UpdateBatchAvailability(List<UpdateAvailability> updates)
        {
            if (updates.Count == 0) return BadRequest();

            var success = AvailabilityService.UpdateMultipleAvailabilities(updates);
            return success
                ? Ok()
                : (IActionResult) BadRequest();
        }
    }
}