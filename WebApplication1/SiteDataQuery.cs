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

        public async Task<SiteData> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "@SELECT 'monitor_Id', 'transactionsOverTime', 'numberOfLogins', 'webpageSpeed', 'errorRate', 'serviceAvailability' FROM 'monitoring' WHERE 'monitor_Id' = @monitor_Id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@monitor_Id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await RealAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<SiteData>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT 'monitor_Id', 'transactionsOverTime', 'numberOfLogins', 'webpageSpeed', 'errorRate', 'serviceAvailability' FROM 'monitoring' ORDER BY 'monitor_Id' DESC LIMIT 10;";
            return await RealAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM 'BlogPost'";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<SiteData>> RealAllAsync(DbDataReader reader)
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
