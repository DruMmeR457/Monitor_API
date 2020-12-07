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
            var response = await client.GetAsync("https://metricsapi20201108200731.azurewebsites.net/api/transaction");
            return response;
        }

        public async Task<HttpResponseMessage> GetOneTransactionAsync(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://metricsapi20201108200731.azurewebsites.net/api/transaction/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> PostTransactionAsync(DateTime timeStamp)
        {
            Transaction transaction = new Transaction(timeStamp);
            var json = await Task.Run(() => JsonConvert.SerializeObject(transaction));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync("https://metricsapi20201108200731.azurewebsites.net/api/transaction/post", row);
            return response;
        }

        public async Task<HttpResponseMessage> PutTransactionAsync(int record, DateTime time_Stamp)
        {
            Transaction transaction = new Transaction(record, time_Stamp);
            var json = await Task.Run(() => JsonConvert.SerializeObject(transaction));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync($"https://metricsapi20201108200731.azurewebsites.net/api/transaction/put/{transaction.Record}", row);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteOneTransactionAsync(int id)
        {
            using var client = new HttpClient();
            var response = await client.DeleteAsync($"https://metricsapi20201108200731.azurewebsites.net/api/transaction/delete/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAllTransactionAsync()
        {
            using var client = new HttpClient();
            var response = await client.DeleteAsync("https://metricsapi20201108200731.azurewebsites.net/api/transaction/delete");
            return response;
        }
    }
}
