using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ParKing.Utils.Configuration;
using Serilog;

namespace ParKing.Application.RaspberryApi.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private Config Config { get; }

        public AuthenticationMiddleware(RequestDelegate next, Config config)
        {
            _next = next;
            Config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                var configuredUsername = Config.AuthUsername;
                var configuredPassword = Config.AuthPassword;
                const string seperator = ":";

                var configuredBytes = Encoding.UTF8.GetBytes(configuredUsername + seperator + configuredPassword);
                var configured64 = Convert.ToBase64String(configuredBytes);
                
                //Extract credentials
                var requestAuth = authHeader.Substring("Basic ".Length).Trim();

                if(configured64 == requestAuth)
                {
                    await _next(context);
                }
                else
                {
                    Log.Logger.Warning($"Unauthorized using auth value '{authHeader}'.  (Original request: {context.Request.Path})");
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
