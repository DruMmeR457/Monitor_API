///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      WebData.cs
/// Description:    
///                 Contains getters and setters for WebData class.
///                 WebData class will contain items inside database.
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
    public class WebData
    {
        public int Record { get; set; }
        public DateTime Time_Stamp { get; set; }
        public double Speed { get; set; }

        internal AppDb Db { get; set; }

        /// <summary>
        /// Default constructor for WebData class
        /// </summary>
        public WebData()
        {

        }

        /// <summary>
        /// Constructor with passed DB object
        /// </summary>
        /// <param name="db">Database</param>
        internal WebData(AppDb db)
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
            cmd.CommandText = @"INSERT INTO Webpage_Status (Time_Stamp, Speed) VALUES (@Time_Stamp, @Speed);";
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
            cmd.CommandText = @"UPDATE Webpage_Status SET Time_Stamp = @Time_Stamp, Speed = @Speed WHERE Record = @Record;";
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
            cmd.CommandText = @"DELETE FROM Webpage_Status WHERE Record = @Record;";
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
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Speed",
                DbType = DbType.Double,
                Value = Speed,
            });
        }
    }
}
