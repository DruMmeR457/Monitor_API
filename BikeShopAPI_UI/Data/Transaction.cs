using System;

namespace BikeShopAPI_UI.Data
{
    public class Transaction
    {
        public int Record { get; set; }
        public DateTime Time_Stamp { get; set; }

        public Transaction()
        {

        }

        public Transaction(DateTime time_Stamp)
        {
            this.Time_Stamp = time_Stamp;
        }

        public Transaction(int record, DateTime time_Stamp)
        {
            this.Record = record;
            this.Time_Stamp = time_Stamp;
        }
    }
}
