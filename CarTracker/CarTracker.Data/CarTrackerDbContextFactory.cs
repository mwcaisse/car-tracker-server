using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Data
{
    public class CarTrackerDbContextFactory
    {
        private readonly DbContextOptions<CarTrackerDbContext> _options;

        public CarTrackerDbContextFactory(DbContextOptions<CarTrackerDbContext> options)
        {
            _options = options;
        }

        public CarTrackerDbContext CreateContext()
        {
            return new CarTrackerDbContext(_options);
        }

    }
}
