using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Places;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Data.Extensions
{
    public static class GooglePlaceExtensions
    {

        public static IQueryable<GooglePlace> Build(this IQueryable<GooglePlace> query)
        {
            return query.Include(g => g.Place);
        }
    }
}
