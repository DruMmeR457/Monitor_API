using System;

namespace BikeShopAPI_UI.Data
{
    public class Login
    {
        public int Record { get; set; }
        public DateTime Time_Stamp { get; set; }
        public String AccountName { get; set; }
        public String AccountType { get; set; }

        public Login()
        {

        }

        public Login(DateTime time_Stamp, String accountName, String accountType)
        {
            this.Time_Stamp = time_Stamp;
            this.AccountName = accountName;
            this.AccountType = accountType;
        }

        public Login(int record, DateTime time_Stamp, String accountName, String accountType)
        {
            this.Record = record;
            this.Time_Stamp = time_Stamp;
            this.AccountName = accountName;
            this.AccountType = accountType;
        }
    }
}
