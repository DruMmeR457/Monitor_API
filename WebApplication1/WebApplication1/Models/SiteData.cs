using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAPI.Models
{
    public class SiteData
    {
        public int Monitor_ID { get; set; }
        public int TransactionsOverTime { get; set; }
        public int NumberOfLogins { get; set; }
        public float WebpageSpeed { get; set; }
        public int ErrorRate { get; set; }
        public bool ServiceAvailability { get; set; }
    }
}
