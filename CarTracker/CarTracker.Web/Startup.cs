using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CarTracker.Common.Auth;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Services;
using CarTracker.Data;
using CarTracker.Logic.Services;
using CarTracker.Web.Auth;
using CarTracker.Web.Configuration;
using CarTracker.Web.Util;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            //Authentication Services
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        if (context.Request.Path.Value.Contains("/api/"))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            context.RedirectUri = options.LoginPath;
                        }
                        return Task.CompletedTask;
                    };
                });


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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPasswordHasher, ArgonPasswordHasher>();

            services.AddTransient<UserAuthenticationManager>();

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

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
