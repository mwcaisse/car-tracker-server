using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Places;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Data.Extensions
{
    public static class UserPlaceExtensions
    {

        public static IQueryable<UserPlace> Build(this IQueryable<UserPlace> query)
        {
            return query.Include(u => u.Place).Include(u => u.Owner);
        }
    }
}
