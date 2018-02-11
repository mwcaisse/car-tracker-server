using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common.Entities.Logging;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Data.Extensions
{
    public static class RequestLogExtensions
    {

        public static IQueryable<RequestLog> Build(this IQueryable<RequestLog> query)
        {
            return query.Include(l => l.User);
        }

        public static IQueryable<RequestLog> Filter(this IQueryable<RequestLog> query,
            Dictionary<string, string> filters)
        {
            foreach (var pair in filters)
            {
                var propertyName = pair.Key;
                if (string.Equals("RequestUrl", propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(rl => rl.RequestUrl.Contains(pair.Value));
                }
                else if (string.Equals("RequestMethod", propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(rl => rl.RequestMethod == pair.Value);
                }
                else if (string.Equals("ResponseStatus", propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(rl => rl.ResponseStatus == pair.Value);
                }

            }

            return query;
        }

    }
}
