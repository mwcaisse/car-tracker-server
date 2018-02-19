using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CarTracker.Common;
using CarTracker.Common.Entities;
using CarTracker.Common.Exceptions;
using CarTracker.Common.ViewModels;

namespace CarTracker.Data.Extensions
{
    public static class LinqExtensions
    {

        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => 
                string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            if (matchedProperty == null)
            {
                throw new SortException($"Invalid column name {name}.");
            }

            return matchedProperty;
        }
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>) genericMethod.Invoke(null, new object[] {query, expr});
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }


        public static PagedViewModel<T> PageAndSort<T>(this IQueryable<T> query, int skip, 
            int take, SortParam sortParam) where T : ITrackedEntity
        {
            if (string.IsNullOrWhiteSpace(sortParam.ColumnName))
            {
                sortParam.ColumnName = "CreateDate";
                sortParam.Ascending = false;
            }

            if (sortParam.Ascending)
            {
                query = query.OrderBy(sortParam.ColumnName);
            }
            else
            {
                query = query.OrderByDescending(sortParam.ColumnName);
            }

            int count = query.Count();

            if (take <= 0 || take > 100)
            {
                throw new EntityValidationException("Invalid page size. Take must be between 1 and 100.");
            }
            if (skip < 0)
            {
                throw new EntityValidationException("Invalid skip. Skip must be >= 0.");
            }

            return new PagedViewModel<T>()
            {
                Data = query.Skip(skip).Take(take),
                Total = count,
                Skip = skip,
                Take = take
            };
        }

        public static IQueryable<T> Active<T>(this IQueryable<T> query) where T : IActiveEntity
        {
            return query.Where(e => e.Active);
        }

        public static Expression<Func<T, bool>> ConstructExpression<T>(string propertyName, string operationName, string value)
        {
            var param = Expression.Parameter(typeof(T), "rl");
            var left = Expression.Property(param, propertyName);
            var right = Expression.Constant(value, typeof(string));

            var propertyType = typeof(T).GetProperty(propertyName).PropertyType;
            if (propertyType == typeof(DateTime))
            {
                var dateValue = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(value)).DateTime;
                right = Expression.Constant(dateValue, typeof(DateTime));
            }
            var exp = Expression.Lambda<Func<T, bool>>(
                ConstructExpressionCondition(operationName, left, right), param
            );
            return exp;
        }

        public static Expression ConstructExpressionCondition(string operationName,
            MemberExpression left, ConstantExpression right)
        {

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
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                return Expression.Call(left, method, right);
            }

            //Default to equals
            return Expression.Equal(left, right);
        }


        public static IQueryable<T> Filter<T>(this IQueryable<T> query,
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

                query = query.Where(ConstructExpression<T>(propertyName, operatorName, pair.Value));
            }

            return query;
        }
    }
}
