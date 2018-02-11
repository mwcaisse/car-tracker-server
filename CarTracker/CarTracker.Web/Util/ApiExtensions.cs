using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    }
}
