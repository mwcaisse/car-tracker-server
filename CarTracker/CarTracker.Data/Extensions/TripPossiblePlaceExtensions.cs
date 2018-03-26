using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Data.Extensions
{
    public static class TripPossiblePlaceExtensions
    {

        public static IQueryable<TripPossiblePlace> Build(this IQueryable<TripPossiblePlace> query)
        {
            return query.Include(t => t.Place).Include(t => t.Trip);
        }
    }
}
