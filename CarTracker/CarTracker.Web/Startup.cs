using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Services;
using CarTracker.Data;
using CarTracker.Logic.Services;
using CarTracker.Web.Configuration;
using CarTracker.Web.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("dbConfig.json")
                .AddJsonFile("apiKeys.json");

            Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddDbContext<CarTrackerDbContext>(
                options => options.UseMySql(Configuration.GetSection("connectionString").Value));

            //Load in some confiugration options
            var googleMapsApiKey = Configuration.GetValue<string>("googleMapsApiKey");

            // Add our Services
            
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<ITripService, TripService>();
            services.AddTransient<IReaderLogService, ReaderLogService>();
            services.AddTransient<IReadingService, ReadingService>();
            services.AddTransient<ITripProcessor, TripProcessor>();
            services.AddTransient<ITripPossiblePlaceService, TripPossiblePlaceService>();
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<ICarSupportedCommandsService, CarSupportedCommandsService>();
            services.AddTransient<IPlaceRequester>(s => new GooglePlaceRequester.GooglePlaceRequester(googleMapsApiKey));

            services.AddSingleton(s => new ApplicationConfiguration()
            {
                GoogleMapsAPiKey = googleMapsApiKey
            });

            services.AddMvc().AddJsonOptions(options =>
                options.SerializerSettings.Converters.Add(new JsonDateEpochConverter())
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
