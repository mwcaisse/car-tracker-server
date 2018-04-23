using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Data.Extensions
{
    public static class TripExtensions
    {

        public static IQueryable<Trip> Build(this IQueryable<Trip> query)
        {
            return query.Include(t => t.Car).Include(t => t.StartPlace).Include(t => t.DestinationPlace);
        }

    }
}
