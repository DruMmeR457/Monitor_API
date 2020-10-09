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
            String answer, switchAnswer;
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5001/api/site");
            var message = await response.Content.ReadAsStringAsync();
            Console.WriteLine("\nCurrently in db:");
            Console.WriteLine(message);
            do
            {
                Console.Write("What records would you like to see (get all, get one)? ");
                answer = Console.ReadLine();
                switch(answer)
                {
                    case "get all":
                        response = await client.GetAsync("http://localhost:5001/api/site");
                        message = await response.Content.ReadAsStringAsync();
                        break;
                    case "get one":
                        Console.WriteLine("Select the Monitor_ID to get: ");
                        switchAnswer = Console.ReadLine();
                        response = await client.GetAsync($"http://localhost:5001/api/site/{switchAnswer}");
                        message = await response.Content.ReadAsStringAsync();
                        break;
                        /*
                    case "delete":
                        Console.WriteLine("Select the Monitor_ID to delete: ");
                        switchAnswer = Console.ReadLine();
                        response = await client.GetAsync($"http://localhost:5001/api/site/delete/{switchAnswer}");
                        message = await response.Content.ReadAsStringAsync();
                        break;
                    case "update":  //this switch is not done
                        response = await client.GetAsync("http://localhost:5001/api/sit/update");
                        message = await response.Content.ReadAsStringAsync();
                        break;
                        */
                    default:
                        answer = "Leave";
                        break;
                }
                Console.WriteLine(message);
            }
            while (!answer.Equals("Leave", StringComparison.OrdinalIgnoreCase));
        }
    }
}
