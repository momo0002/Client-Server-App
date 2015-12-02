using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            string line = "";
            do
            {
                Console.Write("Enter Message: ");
                line = Console.ReadLine();

                if (line != "")
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:23373/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        //HttpResponseMessage response = await client.GetAsync("Home/Index");

                        // HTTP POST
                        //var msg = new Messages() { message = "test", time = "time" };

                        var content = new FormUrlEncodedContent(new[]{
                            new KeyValuePair<string, string>("message", line),
                            new KeyValuePair<string, string>("time", DateTime.Now.ToString()),
                        });

                        try
                        {
                            HttpResponseMessage response = await client.PostAsync("Home/Index", content);
                            response.EnsureSuccessStatusCode();

                            //var responseString = await response.Content.ReadAsStringAsync();

                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine("Error" + e.GetBaseException());
                        }
                    }
                }
            } while (line != "");           
        }
    }
}
