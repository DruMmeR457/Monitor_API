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
