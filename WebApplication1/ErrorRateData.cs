﻿///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      ErrorRateData.cs
/// Description:    
///                 Contains getters and setters for ErrorRateData class.
///                 ErrorRateData class will contain items inside database.
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
/// Created:        Thursday, October 8th, 2020
///
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;


namespace MetricsAPI
{
    public class ErrorRateData
    {
        public int Record { get; set; }
        public DateTime Time_Stamp { get; set; }

        internal AppDb Db { get; set; }

        /// <summary>
        /// Default constructor for ErrorRateData class
        /// </summary>
        public ErrorRateData()
        {

        }

        /// <summary>
        /// Constructor with passed DB object
        /// </summary>
        /// <param name="db">Database</param>
        internal ErrorRateData(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Inserts async into API
        /// </summary>
        /// <returns></returns>
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Error_Rate (Time_Stamp) VALUES (@Time_Stamp);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Record = (int)cmd.LastInsertedId;
        }

        /// <summary>
        /// Update async in the API
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE Error_Rate SET Time_Stamp = @Time_Stamp WHERE Record = @Record;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Delete async in the API
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM Error_Rate WHERE Record = @Record;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Binds ID
        /// </summary>
        /// <param name="cmd"></param>
        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Record",
                DbType = DbType.Int32,
                Value = Record,
            });
        }


        /// <summary>
        /// Binds parameters
        /// </summary>
        /// <param name="cmd"></param>
        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Time_Stamp",
                DbType = DbType.Time,
                Value = Time_Stamp,
            });
        }
    }
}
