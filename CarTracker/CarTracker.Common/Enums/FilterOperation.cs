using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Common.Enums
{
    public enum FilterOperation
    {
        Equal = 1,
        NotEqual,
        LessThanOrEqual,
        GreaterThanOrEqual,
        LessThan,
        GreaterThan,
        Contains
    }

    public static class FilterOperationExtensions
    {

        private static readonly Dictionary<string, FilterOperation> FilterOperationConstants = new Dictionary<string, FilterOperation>()
        {
            {Constants.FilterOperation.Eq, FilterOperation.Equal},
            {Constants.FilterOperation.Ne, FilterOperation.NotEqual },
            {Constants.FilterOperation.Lte, FilterOperation.LessThanOrEqual },
            {Constants.FilterOperation.Gte, FilterOperation.GreaterThanOrEqual },
            {Constants.FilterOperation.Lt, FilterOperation.LessThan },
            {Constants.FilterOperation.Gt, FilterOperation.GreaterThan },
            {Constants.FilterOperation.Cont, FilterOperation.Contains }
        };

        public static FilterOperation FromString(string filterOperation)
        {
            filterOperation = filterOperation.ToLower();
            if (FilterOperationConstants.ContainsKey(filterOperation))
            {
                return FilterOperationConstants[filterOperation];
            }
            throw new ArgumentException($"{filterOperation} is not a valid value for Filter Operation");
        }
    }

}
