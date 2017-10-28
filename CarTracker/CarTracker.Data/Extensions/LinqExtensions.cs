using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
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
            int take, SortParam sortParam)
        {
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
                Count = count,
                Skip = skip,
                Take = take
            };
        }
    }
}
