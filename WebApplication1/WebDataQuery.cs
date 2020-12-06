///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      WebDataQuery.cs
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
/// Created:        Thursday, October 8th, 2020
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
    public class WebDataQuery
    {
        public AppDb Db { get; }

        public WebDataQuery(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Finds one async 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Count but defaults to null</returns>
        public async Task<WebData> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT Record, Time_Stamp, Speed FROM Webpage_Status WHERE Record = @Record;";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Record",
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
        public async Task<List<WebData>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT Record, Time_Stamp, Speed FROM Webpage_Status ORDER BY Record;";
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
            cmd.Transaction = txn;
            cmd.CommandText = @"DELETE FROM Webpage_Status";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        /// <summary>
        /// Resets the auto-incrementation to 1
        /// </summary>
        /// <returns></returns>
        public async Task ResetAuto()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"ALTER TABLE Webpage_Status AUTO_INCREMENT =1;";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Reads all async values
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private async Task<List<WebData>> ReadAllAsync(DbDataReader reader)
        {
            var data = new List<WebData>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var datum = new WebData(Db)
                    {
                        Record = reader.GetInt32(0),
                        Time_Stamp = reader.GetDateTime(1),
                        Speed = reader.GetDouble(2),
                    };
                    data.Add(datum);
                }
            }
            return data;
        }
    }
}
