using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5001/api/site");
            var message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }
}
