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
    public class Error_RateService
    {
        public async Task<HttpResponseMessage> GetError_RateAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5001/api/error");
            return response;
        }

        public async Task<HttpResponseMessage> PostError_RateAsync(DateTime time_Stamp)
        {
            Error_Rate error = new Error_Rate(time_Stamp);
            var json = await Task.Run(() => JsonConvert.SerializeObject(error));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5001/api/error/post", row);
            return response;
        }

        public async Task<HttpResponseMessage> PutError_RateAsync(int record, DateTime time_Stamp)
        {
            Error_Rate error = new Error_Rate(record, time_Stamp);
            var json = await Task.Run(() => JsonConvert.SerializeObject(error));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync($"http://localhost:5001/api/error/put/{error.Record}", row);
            return response;
        }
    }
}
