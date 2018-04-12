using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarTracker.Web.Extensions
{
    public static class CarTrackerDbContextFactoryExtension
    {

        public static IServiceCollection AddCarTrackerDbContextFactory(this IServiceCollection serviceCollection,
            Action<DbContextOptionsBuilder> optionsAction)
        {
            var builder = new DbContextOptionsBuilder<CarTrackerDbContext>();
            optionsAction(builder);
          
            serviceCollection.AddSingleton(new CarTrackerDbContextFactory(builder.Options));
            return serviceCollection;
        }

    }
}
