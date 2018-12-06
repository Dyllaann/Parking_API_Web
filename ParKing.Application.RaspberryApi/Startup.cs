using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParKing.Business.Services;
using ParKing.Data;
using ParKing.Data.Repository;
using ParKing.Utils.Configuration;
using ParKing.Utils.Configuration.Model;
using Swashbuckle.AspNetCore.Swagger;

namespace ParKing.Application.RaspberryApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOptions();
            
            AddSwagger(services);
            AddConfig();
            AddDatabase(services);
            AddDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/swagger/v1/swagger.json", "My API V1");
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

            //Setup MVC and HTTPS
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        #region Private methods
        private static void AddSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ParKing MobileAPI", Version = "v1" });
            });
        }

        private void AddConfig()
        {
            var configSection = Configuration.GetSection("ConfigRoot");
            ServiceCollection.Configure<ConfigRoot>(configSection);
            var config = configSection.Get<ConfigRoot>();
            Config = new Config(config);
            ServiceCollection.AddSingleton(Config);
        }

        private void AddDatabase(IServiceCollection services)
        {
            services.AddDbContext<ParKingContext>(options =>
                options.UseSqlServer(Config.DatabaseConnectionString));
        }

        private static void AddDependencies(IServiceCollection services)
        {
            services.AddTransient<AvailabilityRepository>();
            services.AddTransient<AvailabilityService>();

        }
        #endregion
    }
}
