using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CarTracker.Common.Auth;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Models;
using CarTracker.Common.Services;
using CarTracker.Data;
using CarTracker.Logic.Services;
using CarTracker.Web.Auth;
using CarTracker.Web.Configuration;
using CarTracker.Web.Middleware;
using CarTracker.Web.Model;
using CarTracker.Web.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
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
                .AddJsonFile("apiKeys.json")
                .AddJsonFile("deploymentConfiguration.json");

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
            var rootPathPrefix = Configuration.GetValue<string>("rootPathPrefix", ""); ;

            var applicationConfig = new ApplicationConfiguration()
            {
                GoogleMapsAPiKey = googleMapsApiKey,
                RootPathPrefix = rootPathPrefix
            };
            services.AddSingleton(applicationConfig);

            //Authentication Services
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddTokenAuthentication(options => { })
                .AddCookie(options =>
                {
                    options.LoginPath = applicationConfig.PrefixUrl("/login");
                    options.LogoutPath = applicationConfig.PrefixUrl("/logout");
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        if (context.Request.Path.Value.Contains("/api/"))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            context.Response.Redirect(options.LoginPath);
                        }
                        return Task.CompletedTask;
                    };
                });

            //Add HttpContextAccessor as a Service
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
            services.AddTransient<IRegistrationKeyService, RegistrationKeyService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserAuthenticationTokenService, UserAuthenticationTokenService>();
            services.AddTransient<IPasswordHasher, ArgonPasswordHasher>();

            services.AddScoped<IRequestLogger, RequestLogger>();

            services.AddTransient<UserAuthenticationManager>();
            services.AddSingleton<SessionTokenManager>();

            services.AddMvc().AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new JsonDateEpochConverter());
                    options.SerializerSettings.Converters.Add(new JsonUserConverter());
                }
            );

            services.AddScoped<IRequestInformation, ServerRequestInformation>();

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

            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            //Middleware to use the Session Token Header authentication scheme, if it's header is present
            //  otherwise we use Cookie Authentication Scheme
            app.Use(async (context, next) =>
            {
                var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
                if (TokenAuthenticationOptions.IsRequestCanidate(context))
                {
                    scheme = TokenAuthenticationOptions.AuthenticationScheme;
                }
                var result = await context.AuthenticateAsync(scheme);
                if (result.Succeeded)
                {
                    context.User = result.Principal;
                }
                await next();
            });

            app.UseRequestLoggingMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
