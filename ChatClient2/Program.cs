using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient2
{
    class Program
    {
        static void Main(string[] args)
        {
            var getRequest = new Task<string>(() =>
            {

                Console.WriteLine("Waiting for request!");
                while (true)
                {
                    Thread.Sleep(1000);
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(string.Format("http://localhost:63653/api/CallingRequest?id={0}", 8)).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(content);
                    if (content != "" || content != null || content !="[]")
                        return content;
                }
            }
            );

            getRequest.Start();

            var req = getRequest.Result;

            Console.WriteLine("You have request!");

            Thread.Sleep(1000);

            var client1 = new HttpClient();
            client1.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
            var content1 = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("SenderID", "8"),
                new KeyValuePair<string, string>("ReceiverID", "1"),
                new KeyValuePair<string, string>("State", "2"),
            });
            var response1 = client1.PostAsync("http://localhost:63653/api/CallingRequest", content1).Result;
            var content2 = response1.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content2);



            var receiveMessage = new Task(() =>
            {

                Console.WriteLine("wait");
                while (true)
                {
                    Thread.Sleep(1000);
                    var client33 = new HttpClient();
                    client33.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var response22 = client33.GetAsync(string.Format("http://localhost:63653/api/CallProcess?senderID={0}&receiverID={1}", 1, 8)).Result;
                    var content22 = response22.Content.ReadAsStringAsync().Result;
                    
                    if (content22 != null || content22 != "" || content22 != "[]")
                        Console.WriteLine(content22); 
                }
            }
            );


            var sendMessage = new Task(() =>
            {
                while (true)
                {
                    Console.WriteLine("Enter message");
                    var message = Console.ReadLine();

                    var client8 = new HttpClient();
                    client8.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var content8 = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("SenderID", "8"),
                    new KeyValuePair<string, string>("ReceiverID", "1"),
                    new KeyValuePair<string, string>("Traffic", message),
                    });
                    var response8 = client8.PostAsync("http://localhost:63653/api/CallProcess", content8).Result;
                    var content9 = response8.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(content9);

                }
            }
            );

            sendMessage.Start();

            receiveMessage.Start();

            Task.WaitAll(sendMessage, receiveMessage);
        }
    }
}
