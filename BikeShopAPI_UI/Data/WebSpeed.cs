using System;

namespace BikeShopAPI_UI.Data
{
    public class WebSpeed
    {
        public int Record { get; set; }
        public DateTime Time_Stamp { get; set; }
        public double Speed { get; set; }

        public WebSpeed()
        {

        }

        public WebSpeed(DateTime time_Stamp, double speed)
        {
            this.Time_Stamp = time_Stamp;
            this.Speed = speed;
        }

        public WebSpeed(int record, DateTime time_Stamp, double speed)
        {
            this.Record = record;
            this.Time_Stamp = time_Stamp;
            this.Speed = speed;
        }
    }
}
