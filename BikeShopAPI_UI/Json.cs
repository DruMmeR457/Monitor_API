///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      Json.cs
/// Description:
///                 Class used to hold contents of json objects obtained
///                 from API
///                 
/// Course:         CSCI 4350 - Software Engineering
/// Authors:
///                 Darien Roach,   roachda@etsu.edu,   Developer
///                 Grant Watson,   watsongo@etsu.edu,  Developer
///                 Kelly King,     kingkr1@etsu.edu,   Developer
///                 Jackson Brooks, brooksjt@etsu.edu,  Developer
///                 Nick Ehrhart,   ehrhart@etsu.edu,   Product Owner
///                 Anna Cade,      cadea1@etsu.edu,    Scrum Master
///
/// Created:        Sunday, October 18th, 2020
///
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BikeShopAPI_UI
{
    public class Json
    {
        public int Record { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Speed { get; set; }
        public string strName { get; set; }
        public string strType { get; set; }

        public Json()
        {

        }

        public Json(int R, DateTime DT, double S)
        {
            Record = R;
            TimeStamp = DT;
            Speed = S;
        }

        public Json(int R, DateTime DT, string name, string type)
        {
            Record = R;
            TimeStamp = DT;
            strName = name;
            strType = type;
        }
    }
}
