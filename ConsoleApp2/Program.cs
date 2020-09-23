using System;
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
            var response = await client.GetAsync("http://localhost:5001/test");
            var message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }
}
