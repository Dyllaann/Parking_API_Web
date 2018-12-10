﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParKing.Business.Services;
using ParKing.Data.Engine;
using ParKing.Utils.Configuration;
using Serilog;

namespace ParKing.Application.RaspberryApi.Controllers
{
    [Route("api/[controller]")]
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
            Log.Logger.Information($"Received request at {Request.Path}");
            if (update == null) return BadRequest();
            
            var success = AvailabilityService.UpdateSingleAvailability(update);
            return success == UpdateStatus.Ok
                ? Ok()
                : (IActionResult) BadRequest();
        }

        [HttpPatch]
        [Route("batch")]
        public IActionResult UpdateBatchAvailability(List<UpdateAvailability> updates)
        {
            if (updates.Count == 0) return BadRequest();

            var success = AvailabilityService.UpdateMultipleAvailabilities(updates);
            return success
                ? Ok()
                : (IActionResult) BadRequest();
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> TestCall()
        {
            var httpclient = new HttpClient();
            var x = await httpclient.GetAsync("localhost:8001/api/Test");

            return Ok(await x.Content.ReadAsStringAsync());
        }
    }
}
