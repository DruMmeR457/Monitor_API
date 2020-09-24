﻿///////////////////////////////////////////////////////////////////////////
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
using MySqlConnector;
using System.Data;


namespace MetricsAPI
{
    public class SiteData
    {

        public int Monitor_ID { get; set; }
        public int TransactionsOverTime { get; set; }
        public int NumberOfLogins { get; set; }
        public decimal WebpageSpeed { get; set; }
        public int ErrorRate { get; set; }
        public int ServiceAvailability { get; set; }

        internal AppDb Db { get; set; }

        /// <summary>
        /// Default constructor for SiteData class
        /// </summary>
        public SiteData()
        {

        }

        /// <summary>
        /// Constructor with passed DB object
        /// </summary>
        /// <param name="db">Database</param>
        internal SiteData(AppDb db)
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
            cmd.CommandText = @"INSERT INTO monitoring (transactionsOverTime, numberOfLogins, webpageSpeed, errorRate, serviceAvailability) VALUES (@transactionsOverTime, @numberOfLogins, @webpageSpeed, @errorRate, @serviceAvailability);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Monitor_ID = (int)cmd.LastInsertedId;
        }

        /// <summary>
        /// Update async in the API
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE monitoring SET transactionsOverTime = @transactionsOverTime, numberOfLogins = @numberOfLogins, webpageSpeed = @webpageSpeed, errorRate = @errorRate, serviceAvailability = @serviceAvailability WHERE 'monitor_ID' = @monitor_ID;";
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
            cmd.CommandText = @"DELETE FROM monitoring WHERE monitor_ID = @monitor_ID;";
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
                ParameterName = "@monitor_ID",
                DbType = DbType.Int32,
                Value = Monitor_ID,
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
                ParameterName = "@transactionsOverTime",
                DbType = DbType.Int32,
                Value = TransactionsOverTime,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@numberOfLogins",
                DbType = DbType.Int32,
                Value = NumberOfLogins,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@webpageSpeed",
                DbType = DbType.Decimal,
                Value = WebpageSpeed,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@errorRate",
                DbType = DbType.Int32,
                Value = ErrorRate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@serviceAvailability",
                DbType = DbType.Int32,
                Value = ServiceAvailability,
            });
        }
    }
}
