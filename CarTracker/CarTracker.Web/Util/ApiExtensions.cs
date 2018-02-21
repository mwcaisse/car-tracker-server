using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTracker.Common.Enums;
using CarTracker.Common.Exceptions;
using CarTracker.Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarTracker.Web.Util
{
    public static class ApiExtensions
    {

        private static readonly IEnumerable<string> SortPageParams = new List<string>()
        {
            "skip",
            "take",
            "columnName",
            "ascending"
        };

        /// <summary>
        ///  Cleans Filter Parameters, removes any sorting/paging params from the dictionary.
        ///  Returns a new case-insensitive dictionary
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="additionalToRemove"></param>
        /// <returns></returns>
        public static Dictionary<string, string> CleanFilterParameters(this Dictionary<string, string> filters,
            IEnumerable<string> additionalToRemove = null )
        {
            filters = filters.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);

            if (null == additionalToRemove)
            {
                additionalToRemove = new List<string>();
            }
            foreach (var key in additionalToRemove.Concat(SortPageParams))
            {
                filters.Remove(key);
            }
            return filters;
        }

        public static IEnumerable<FilterParam> ConvertToFilterParams(this Dictionary<string, string> filters,
            IEnumerable<string> additionalToRemove = null)
        {
            if (null == filters || !filters.Any())
            {
                return new List<FilterParam>();
            }

            filters = filters.CleanFilterParameters(additionalToRemove);
            var filterParams = new List<FilterParam>();

            foreach (var filter in filters)
            {
                var tokens = filter.Key.Split("__");
                if (tokens.Length != 2)
                {
                    throw new QueryException($"Filter {filter.Key} is in an unreconized format.");
                }
                filterParams.Add(new FilterParam()
                {
                    ColumnName = tokens.First(),
                    Operation = FilterOperationExtensions.FromString(tokens.Last()),
                    Value = filter.Value
                });
            }

            return filterParams;
        }

    }
}
