using Microsoft.AspNetCore.Components;
using System;

namespace BikeShopAPI_UI.Data
{
    public class Transaction
    {
        [Parameter]
        public int Record { get; set; }
        [Parameter]
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
