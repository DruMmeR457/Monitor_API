using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace BikeShopAPI_UI.Data
{
    public class TransactionService
    {
        public async Task<HttpResponseMessage> GetTransactionAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5001/api/transaction");
            return response;
        }

        public async Task<HttpResponseMessage> GetOneTransactionAsync(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5001/api/transaction/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> PostTransactionAsync(DateTime timeStamp)
        {
            Transaction transaction = new Transaction(timeStamp);
            var json = await Task.Run(() => JsonConvert.SerializeObject(transaction));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5001/api/transaction/post", row);
            return response;
        }

        public async Task<HttpResponseMessage> PutTransactionAsync(int record, DateTime time_Stamp)
        {
            Transaction transaction = new Transaction(record, time_Stamp);
            var json = await Task.Run(() => JsonConvert.SerializeObject(transaction));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync($"http://localhost:5001/api/transaction/put/{transaction.Record}", row);
            return response;
        }
    }
}
