﻿///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      LoginCount.cs
/// Description:
///                 Contains counter class
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

namespace BikeShopAPI_UI
{
    public class LoginCount
    {
        public int LogCount { get; set; }

        public LoginCount()
        {
            LogCount = 0;
        }

        public void incCount()
        {
            LogCount++;
        }
    }
}
