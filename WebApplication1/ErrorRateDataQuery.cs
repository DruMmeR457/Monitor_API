///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 2
/// File Name:      ErrorRateDataQuery.cs
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
    public class ErrorRateDataQuery
    {
        public AppDb Db { get; }

        public ErrorRateDataQuery(AppDb db)
        {
            Db = db;
        }

        /// <summary>
        /// Finds one async 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Count but defaults to null</returns>
        public async Task<ErrorRateData> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT Record, Time_Stamp FROM Error_Rate WHERE Record = @Record;";
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
        public async Task<List<ErrorRateData>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT Record, Time_Stamp FROM Error_Rate ORDER BY Record;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        /// <summary>
        /// Deletes all async
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllAsync()  //work in progress
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM Error_Rate";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        /// <summary>
        /// Reads all async values
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private async Task<List<ErrorRateData>> ReadAllAsync(DbDataReader reader)
        {
            var data = new List<ErrorRateData>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var datum = new ErrorRateData(Db)
                    {
                        Record = reader.GetInt32(0),
                        Time_Stamp = reader.GetDateTime(1),
                    };
                    data.Add(datum);
                }
            }
            return data;
        }
    }
}
