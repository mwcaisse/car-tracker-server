using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarTracker.Common;
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
                var tokens = pair.Key.Split("__");
                if (tokens.Length != 2)
                {
                    continue;
                }

                var propertyName = tokens[0];
                var operatorName = tokens[1];

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
                else if (string.Equals("CreateDate", propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    var dateValue = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(pair.Value)).DateTime;
                    if (string.Equals(operatorName, Constants.FilterOperation.Gte))
                    {
                        query = query.Where(rl => rl.CreateDate >= dateValue);
                    }
                    else if (string.Equals(operatorName, Constants.FilterOperation.Lte))
                    {
                        query = query.Where(rl => rl.CreateDate <= dateValue);
                    }
                }

            }

            return query;
        }

    }
}
