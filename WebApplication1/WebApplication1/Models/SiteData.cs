///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 1
/// File Name:      SiteData.cs
/// Description:    
///                 Contains getters and setters for SiteData class.
///                 SiteData class will contain items inside database.
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
