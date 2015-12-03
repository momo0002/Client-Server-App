using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //http://stackoverflow.com/questions/10200910/create-hello-world-websocket-example
    //https://msdn.microsoft.com/en-us/library/debx8sh9(v=vs.110).aspx
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            string line = null;
            while((line = Console.ReadLine()) != null)
            {
                Console.Write("Enter Message: ");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:23373/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP POST
                    //var msg = new Messages() { message = "test", time = "time" };

                    var content = new FormUrlEncodedContent(new[]{
                        new KeyValuePair<string, string>("message", line),
                        new KeyValuePair<string, string>("time", DateTime.Now.ToString()),
                    });

                    try
                    {
                        //HttpResponseMessage response = await client.PostAsync("Home/Index", content);
                        await client.PostAsync("Home/Index", content).ContinueWith(task =>
                        {
                            var response = task.Result;
                            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                        });

                        //var responseString = await response.Content.ReadAsStringAsync();
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("Error" + e.GetBaseException());
                    }
                }
            }
        }
    }
}






    /*
static void Main(string[] args)
{
    //Createa webrequest
    WebRequest request = WebRequest.Create("http://localhost:23373/Home/Index");

    // Optional in this proj
    // setup Credentials
    request.Credentials = CredentialCache.DefaultCredentials;

    //Proprety of request
    request.Method = "POST";

    string line = null;
    while ((line = System.Console.ReadLine()) != null)
    {
        string postData = ("&message=" + line + "&time=" + DateTime.Now);
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        // Set the ContentType property of the WebRequest.
        request.ContentType = "application/x-www-form-urlencoded";
        // Set the ContentLength property of the WebRequest.
        request.ContentLength = byteArray.Length;
        // Get the request stream.
        Stream dataStream = request.GetRequestStream();
        // Write the data to the request stream.
        dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.
        dataStream.Close();
        // Get the response.
        WebResponse response = request.GetResponse();
        // Display the status.
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.
        dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.
        string responseFromServer = reader.ReadToEnd();
        // Display the content.
        Console.WriteLine(responseFromServer);
        // Clean up the streams.
        reader.Close();
        dataStream.Close();
        response.Close();
    }
    Console.ReadLine();
}*/