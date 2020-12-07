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
    public class WebSpeedService
    {
        public async Task<HttpResponseMessage> GetWebSpeedAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://metricsapi20201108200731.azurewebsites.net/api/web");
            return response;
        }

        public async Task<HttpResponseMessage> GetOneWebSpeedAsync(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://metricsapi20201108200731.azurewebsites.net/api/web/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> PostWebSpeedAsync(DateTime timeStamp, double speed)
        {
            WebSpeed webSpeed = new WebSpeed(timeStamp, speed);
            var json = await Task.Run(() => JsonConvert.SerializeObject(webSpeed));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync("https://metricsapi20201108200731.azurewebsites.net/api/web/post", row);
            return response;
        }

        public async Task<HttpResponseMessage> PutWebSpeedAsync(int record, DateTime time_Stamp, double speed)
        {
            WebSpeed webSpeed = new WebSpeed(record, time_Stamp, speed);
            var json = await Task.Run(() => JsonConvert.SerializeObject(webSpeed));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync($"https://metricsapi20201108200731.azurewebsites.net/api/web/put/{webSpeed.Record}", row);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteOneWebSpeedAsync(int id)
        {
            using var client = new HttpClient();
            var response = await client.DeleteAsync($"https://metricsapi20201108200731.azurewebsites.net/api/web/delete/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAllWebSpeedAsync()
        {
            using var client = new HttpClient();
            var response = await client.DeleteAsync("https://metricsapi20201108200731.azurewebsites.net/api/web/delete");
            return response;
        }
    }
}
