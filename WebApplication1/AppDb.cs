///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 1
/// File Name:      AppDb.cs
/// Description:    
///                 Serves to create the database and needed connection
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
using MySqlConnector;

namespace MetricsAPI
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        /// <summary>
        /// Constructor with connection passed
        /// </summary>
        /// <param name="connectionString"></param>
        public AppDb (string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
