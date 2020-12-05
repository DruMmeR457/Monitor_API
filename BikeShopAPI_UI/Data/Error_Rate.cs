using System;

namespace BikeShopAPI_UI.Data
{
    public class Error_Rate
    {
        public int Record { get; set; }
        public DateTime Time_Stamp { get; set; }

        public Error_Rate()
        {

        }

        public Error_Rate(DateTime time_Stamp)
        {
            this.Time_Stamp = time_Stamp;
        }

        public Error_Rate(int record, DateTime time_Stamp)
        {
            this.Record = record;
            this.Time_Stamp = time_Stamp;
        }
    }
}
