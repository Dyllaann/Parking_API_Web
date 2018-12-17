using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ParKing.Application.MobileApi.Middleware;
using ParKing.Business.Services;
using ParKing.Data;
using ParKing.Data.Repository;
using ParKing.Utils.Configuration;
using ParKing.Utils.Configuration.Model;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace ParKing.Application.MobileApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IServiceCollection ServiceCollection { get; set; }
        private Config Config { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceCollection = services;
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("localhost"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOptions();
            services.AddEntityFrameworkSqlServer();

            AddConfig(services);
            AddLogging();
            AddSwagger(services);
            AddDatabase(services);
            AddDependencies(services);
            AddCORS(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            EnableSwagger(app, env);

            //app.UseMiddleware<>();

            //Setup MVC and HTTPS
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
        }

        #region Registration of Services

        private void AddLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Logzio(Config.LogzIoToken, 5, new TimeSpan(0, 0, 0, 10))
                .CreateLogger();
        }

        private static void AddSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "ParKing MobileAPI", Version = "v1"});
            });
        }

        private void AddConfig(IServiceCollection services)
        {
            var configSection = Configuration.GetSection("ConfigRoot");
            ServiceCollection.Configure<ConfigRoot>(configSection);
            var config = configSection.Get<ConfigRoot>();
            Config = new Config(config);
            ServiceCollection.AddSingleton(Config);
            services.AddSingleton(config);
        }

        private void AddDatabase(IServiceCollection services)
        {

            services.AddDbContext<ParKingContext>(
                (serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseMySql(Config.DatabaseConnectionString);
                    optionsBuilder.UseInternalServiceProvider(serviceProvider);
                });
        }

        private static void AddDependencies(IServiceCollection services)
        {
            //Register HttpClients
            services.AddHttpClient<AuthenticationMiddleware>();

            //Register Repositories
            services.AddTransient<ParkingLotRepository>();
            services.AddTransient<ParkingAvailabilityRepository>();

            //Register Services
            services.AddTransient<AvailabilityService>();

        }

        private static void EnableSwagger(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => { swaggerDoc.BasePath = string.Empty; });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            var environment = Environment.GetEnvironmentVariable("env");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "docs";
            });

            //Set develop settings
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
        }

        private static void AddCORS(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });
        }

        #endregion
    }
}
