///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 1
/// File Name:      SiteDataQuery.cs
/// Description:    
///                 Used to query database
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
using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace MetricsAPI
{
    public class SiteDataQuery
    {
        public AppDb Db { get; }

        public SiteDataQuery(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Finds one async 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Count but defaults to null</returns>
        public async Task<SiteData> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT monitor_ID, transactionsOverTime, numberOfLogins, webpageSpeed, errorRate, serviceAvailability FROM monitoring WHERE monitor_ID = @monitor_ID";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@monitor_ID",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        /// <summary>
        /// Obtains latest Posts
        /// </summary>
        /// <returns></returns>
        public async Task<List<SiteData>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT monitor_ID, transactionsOverTime, numberOfLogins, webpageSpeed, errorRate, serviceAvailability FROM monitoring ORDER BY monitor_ID;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        /// <summary>
        /// Deletes all async
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM monitoring";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        /// <summary>
        /// Reads all async values
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private async Task<List<SiteData>> ReadAllAsync(DbDataReader reader)
        {
            var data = new List<SiteData>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var datum = new SiteData(Db)
                    {
                        Monitor_ID = reader.GetInt32(0),
                        TransactionsOverTime = reader.GetInt32(1),
                        NumberOfLogins = reader.GetInt32(2),
                        WebpageSpeed = reader.GetDecimal(3),
                        ErrorRate = reader.GetInt32(4),
                        ServiceAvailability = reader.GetInt32(5)
                    };
                    data.Add(datum);
                }
            }
            return data;
        }
    }
}
