using IdentityModel.Client;
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
            var identityClient = new DiscoveryClient("http://10.27.249.82:59447"); //discover the IdentityServer
            identityClient.Policy.RequireHttps = false;

            var identityServer = identityClient.GetAsync().Result;

            if (identityServer.IsError)
            {
                Console.Write(identityServer.Error);
                return;
            }

            Console.Write("Enter Username 8: ");
            var username = Console.ReadLine();
            Console.Write("Enter Password : ");
            var password = Console.ReadLine();

            //Console.WriteLine(identityServer.TokenEndpoint);

            //Get the token
            var tokenClient = new TokenClient(identityServer.TokenEndpoint, "ChatClient", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync(username, password, "CallingRequestAPI").Result;
            //Console.WriteLine(tokenResponse.AccessToken);


            var getRequest = new Task<string>(() =>
            {

                Console.WriteLine("Waiting for request!");
                while (true)
                {
                    Thread.Sleep(1000);
                    var client = new HttpClient();
                    client.SetBearerToken(tokenResponse.AccessToken);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(string.Format("http://10.27.249.82:63653/api/CallingRequest?id={0}", 8)).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(content);
                    if (content != "" && content != null && content != "[]")
                    {
                        var response56 = client.DeleteAsync(string.Format("http://10.27.249.82:63653/api/CallingRequest?senderID={0}&receiverID={1}", 1, 8)).Result;
                        var content56 = response56.Content.ReadAsStringAsync().Result;

                        return content;
                    }
                }
            }
            );

            getRequest.Start();

            var req = getRequest.Result;

            Console.WriteLine(req);

            Console.WriteLine("You have request!");

            Thread.Sleep(1000);

            var client1 = new HttpClient();
            client1.SetBearerToken(tokenResponse.AccessToken);
            client1.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
            var content1 = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("SenderID", "8"),
                new KeyValuePair<string, string>("ReceiverID", "1"),
                new KeyValuePair<string, string>("State", "2"),
            });
            var response1 = client1.PostAsync("http://10.27.249.82:63653/api/CallingRequest", content1).Result;
            var content2 = response1.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content2);



            var receiveMessage = new Task(() =>
            {

                Console.WriteLine("wait");
                while (true)
                {
                    Thread.Sleep(1000);
                    var client33 = new HttpClient();

                      client33.SetBearerToken(tokenResponse.AccessToken);
                    client33.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var response22 = client33.GetAsync(string.Format("http://10.27.249.82:63653/api/CallProcess?senderID={0}&receiverID={1}", 1, 8)).Result;
                    var content22 = response22.Content.ReadAsStringAsync().Result;
                    
                    if (content22 != null && content22 != "" && content22 != "[]" && content22 != "null")
                        Console.WriteLine(content22.Split(',')[2].Substring(content22.Split(',')[2].LastIndexOf(':')).Replace("\"","").Replace(":", "").Replace("}", ""));

                    var response28 = client33.DeleteAsync(string.Format("http://10.27.249.82:63653/api/CallProcess?senderID={0}&receiverID={1}", 1, 8)).Result;
                    var content28 = response28.Content.ReadAsStringAsync().Result;
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
                    
                    client8.SetBearerToken(tokenResponse.AccessToken);
                    client8.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var content8 = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("SenderID", "8"),
                    new KeyValuePair<string, string>("ReceiverID", "1"),
                    new KeyValuePair<string, string>("Traffic", message),
                    });
                    var response8 = client8.PostAsync("http://10.27.249.82:63653/api/CallProcess", content8).Result;
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
