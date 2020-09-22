///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 1
/// File Name:      MetricContext.cs
/// Description:    
///                 Metric Context class and respective methods
/// Course:         CSCI 4350 - Software Engineering
/// Authors:        
///                 Darien Roach,   roachda@etsu.edu,   Developer
///                 Grant Watson,   watsongo@etsu.edu,  Developer
///                 Stephen Dalton, daltonsa@etsu.edu,  Developer
///                 Kelly King,     kingkr1@etsu.edu,   Developer
///                 Jackson Brooks, brooksjt@etsu.edu,  Developer
///                 Nick Ehrhart,   ehrhart@etsu.edu,   Product Owner
///                 Anna Cade,      cadea1@etsu.edu,    Scrum Master
///                 
/// Created:        Monday, September 14th, 2020
///
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MetricsAPI.Models
{
    public class MetricContext : DbContext
    {
        public MetricContext(DbContextOptions<MetricContext> options)
            : base(options)
        {
        }

        public DbSet<SiteData> SiteData { get; set; }
    }
}
