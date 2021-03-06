﻿///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      LoginModel.cs
/// Description:    
///                 Basic class used for text input in UI
/// Course:         CSCI 4350 - Software Engineering
/// Authors:        
///                 Darien Roach,   roachda@etsu.edu,   Developer
///                 Grant Watson,   watsongo@etsu.edu,  Developer
///                 Kelly King,     kingkr1@etsu.edu,   Developer
///                 Jackson Brooks, brooksjt@etsu.edu,  Developer
///                 Nick Ehrhart,   ehrhart@etsu.edu,   Product Owner
///                 Anna Cade,      cadea1@etsu.edu,    Scrum Master
///                 
/// Created:        Sunday, October 16th, 2020
///
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAPI_UI
{
    public class LoginModel
    {
        [Required]
        public string Input { get; set; }

        public string Description { get; set; }

        [Required]
        public string Password { get; set; }

        public LoginModel()
        {
            Description = "User Name";
        }

    }
}
