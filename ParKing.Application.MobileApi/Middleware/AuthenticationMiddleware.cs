using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ParKing.Utils.Configuration;
using Serilog;

namespace ParKing.Application.MobileApi.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly HttpClient _httpClient;
        private readonly RequestDelegate _next;
        private Config Config { get; }

        public AuthenticationMiddleware(HttpClient httpClient, RequestDelegate next, Config config)
        {
            _httpClient = httpClient;
            _next = next;
            Config = config;
        }   

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {

                

                if (true == true)
                {
                    await _next(context);
                }
                else
                {
                    Log.Logger.Warning($"Unauthorized.");
                    context.Response.StatusCode = 401; //Unauthorized
                }
            }
            else
            {
                // no authorization header
                Log.Logger.Warning($"Lacking authorization header. (Original request: {context.Request.Path})");
                context.Response.StatusCode = 401; //Unauthorized
            }
        }
    }

}
