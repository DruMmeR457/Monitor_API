﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace BikeShopAPI_UI.Data
{
    public class LoginService
    {
        public async Task<HttpResponseMessage> GetLoginAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5001/api/login");
            return response;
        }

        public async Task<HttpResponseMessage> GetOneLoginAsync(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5001/api/login/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> PostLoginAsync(DateTime time_Stamp, String accountName, String accountType)
        {
            Login login = new Login(time_Stamp, accountName, accountType);
            var json = await Task.Run(() => JsonConvert.SerializeObject(login));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5001/api/login/post", row);
            return response;
        }

        public async Task<HttpResponseMessage> PutLoginAsync(int record, DateTime time_Stamp, String accountName, String accountType)
        {
            Login login = new Login(record, time_Stamp, accountName, accountType);
            var json = await Task.Run(() => JsonConvert.SerializeObject(login));

            HttpContent row = new StringContent(json, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync($"http://localhost:5001/api/login/put/{login.Record}", row);
            return response;
        }
    }
}