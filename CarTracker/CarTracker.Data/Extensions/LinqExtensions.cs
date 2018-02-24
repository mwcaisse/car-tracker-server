using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CarTracker.Common;
using CarTracker.Common.Entities;
using CarTracker.Common.Enums;
using CarTracker.Common.Exceptions;
using CarTracker.Common.ViewModels;
using Microsoft.Extensions.Logging;

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
                throw new QueryException($"Invalid column name {name}.");
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

        public static Expression<Func<T, bool>> ConstructExpression<T>(string propertyName, FilterOperation operation, string value)
        {
            var param = Expression.Parameter(typeof(T), "rl");
            var left = Expression.Property(param, propertyName);
            var right = Expression.Constant(value, typeof(string));

            var propertyType = GetPropertyInfo(typeof(T), propertyName).PropertyType;
            if (propertyType == typeof(DateTime))
            {
                var dateValue = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(value)).DateTime.ToLocalTime();
                right = Expression.Constant(dateValue, typeof(DateTime));
            }
            else if (propertyType.IsEnum)
            {
                var intValue = Convert.ToInt32(value);
                var enumValue = Enum.ToObject(propertyType, intValue);
                if (!Enum.IsDefined(propertyType, enumValue))
                {
                    throw new QueryException($"Invalid value for {propertyName}");
                }
                right = Expression.Constant(enumValue, propertyType);
            }
            var exp = Expression.Lambda<Func<T, bool>>(
                ConstructExpressionCondition(operation, left, right), param
            );
            return exp;
        }

        public static Expression ConstructExpressionCondition(FilterOperation operation,
            MemberExpression left, ConstantExpression right)
        {

            switch (operation)
            {
                case FilterOperation.Equal:
                    return Expression.Equal(left, right);
                case FilterOperation.NotEqual:
                  return Expression.NotEqual(left, right);
                case FilterOperation.LessThanOrEqual:
                    return Expression.LessThanOrEqual(left, right);
                case FilterOperation.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(left, right);
                case FilterOperation.LessThan:
                    return Expression.LessThan(left, right);
                case FilterOperation.GreaterThan:
                    return Expression.GreaterThan(left, right);
                case FilterOperation.Contains:
                    if (left.Type != typeof(string))
                    {
                        throw new QueryException("Filter Operation Contains can only be used on strings.");
                    }
                    MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    return Expression.Call(left, method, right);
            }
            //Default to equals
            return Expression.Equal(left, right);
        }


        public static IQueryable<T> Filter<T>(this IQueryable<T> query,
            IEnumerable<FilterParam> filters)
        {
            var filterList = filters as IList<FilterParam> ?? filters.ToList();
            if (filterList.Any())
            {
                foreach (var filter in filterList)
                {
                    query = query.Where(ConstructExpression<T>(filter.ColumnName, filter.Operation, filter.Value));
                }
            }
            return query;
        }
    }
}
