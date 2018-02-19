using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public static Expression<Func<RequestLog, bool>>  ConstructExpression(string propertyName, string operationName, string value)
        {
            var param = Expression.Parameter(typeof(RequestLog), "rl");
            var left = Expression.Property(param, propertyName);
            var right = Expression.Constant(value, typeof(string));
            if (string.Equals("CreateDate", propertyName, StringComparison.OrdinalIgnoreCase))
            {
                var dateValue = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(value)).DateTime;
                right = Expression.Constant(dateValue, typeof(DateTime));
            }
            var exp = Expression.Lambda<Func<RequestLog, bool>>(
                ConstructExpressionCondition(operationName, left, right), param
            );
            return exp;
        }

        public static Expression ConstructExpressionCondition(string operationName, 
            MemberExpression left, ConstantExpression right) {

            if (string.Equals(Constants.FilterOperation.Ne, operationName, StringComparison.OrdinalIgnoreCase))
            {
                return Expression.NotEqual(left, right);
            }
            if (string.Equals(Constants.FilterOperation.Lte, operationName, StringComparison.OrdinalIgnoreCase))
            {
                return Expression.LessThan(left, right);
            }
            if (string.Equals(Constants.FilterOperation.Gte, operationName, StringComparison.OrdinalIgnoreCase))
            {
                return Expression.GreaterThanOrEqual(left, right);
            }
            if (string.Equals(Constants.FilterOperation.Lt, operationName, StringComparison.OrdinalIgnoreCase))
            {
                return Expression.LessThan(left, right);
            }
            if (string.Equals(Constants.FilterOperation.Gt, operationName, StringComparison.OrdinalIgnoreCase))
            {
                return Expression.GreaterThan(left, right);
            }
            if (string.Equals(Constants.FilterOperation.Cont, operationName, StringComparison.OrdinalIgnoreCase))
            {
                //construct the contains method
                MethodInfo method = typeof(string).GetMethod("Contains", new[] {typeof(string)});
                return Expression.Call(left, method, right);
            }

            //Default to equals
            return Expression.Equal(left, right);
        }


        public static IQueryable<RequestLog> Filter(this IQueryable<RequestLog> query,
            Dictionary<string, string> filters)
        {
            foreach (var pair in filters)
            {
                var tokens = pair.Key.Split("__");
                if (tokens.Length != 2)
                {
                    //ignore any filter if it isn't formatted correctly
                    continue;
                }

                var propertyName = tokens[0];
                var operatorName = tokens[1];

                query = query.Where(ConstructExpression(propertyName, operatorName, pair.Value));
            }

            return query;
        }

    }
}
