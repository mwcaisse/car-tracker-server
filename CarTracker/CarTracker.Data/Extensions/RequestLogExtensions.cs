﻿using System;
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
    }
}
