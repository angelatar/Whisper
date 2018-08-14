using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using UserAPI.Businness;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                //var repository = new UsersRepository();
                //var user = repository.GetUserByUsername("BeeBuu");

                //Console.WriteLine(user.ID+" "+user.Name);

                //user = repository.GetUserByID(1);
                //Console.WriteLine(user.ID + " " + user.Name);
                //MainAsync();

                //var bee = new ValidationRepository();
                //bee.CheckValidationCode("bee", "bee");
                //var client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(
                //     new MediaTypeWithQualityHeaderValue("application/json"));

                //var response = client.DeleteAsync("http://192.168.88.21:61366/api/Validation?email=bee").Result;
                //var content = response.Content.ReadAsStringAsync().Result;
                //var bee = JsonConvert.DeserializeObject(content);
                //Console.WriteLine(content);

                //var client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(
                //     new MediaTypeWithQualityHeaderValue("application/json"));
                //var content1 = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("ID", "0"),
                //    new KeyValuePair<string, string>("Name", "bee2"),
                //    new KeyValuePair<string, string>("Lastname", "beebee2"),
                //    new KeyValuePair<string, string>("Username", "beee"),
                //    new KeyValuePair<string, string>("PasswordHash", "bee2"),
                //    new KeyValuePair<string, string>("Email", "bee2")
                //});
                ////var response = client.GetAsync("http://192.168.88.21:61366/api/Register").Result;
                //var response = client.PostAsync("http://192.168.88.21:61366/api/Register",content1).Result;// client.GetAsync(builder.Uri).Result;
                //var content = response.Content.ReadAsStringAsync().Result;
                //var bee = JsonConvert.DeserializeObject(content);
                //Console.WriteLine(content);
                //var bee = Send("angelatarjimanyan@gmail.com","bee");
                //Console.WriteLine(bee);
                //Console.ReadKey();
            }

            //var prkey = Ecc.ECPrivateKey.Create(new Ecc.ECCurve());
            {
                Console.WriteLine("Enter receiver name");
                var receiver = Console.ReadLine();

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));
                var content1 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("SenderID", "1"),
                    new KeyValuePair<string, string>("ReceiverID", "8"),
                    new KeyValuePair<string, string>("State", "1"),
                });
                var response = client.PostAsync("http://localhost:63653/api/CallingRequest", content1).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);

                var getRequest = new Task<string>(() =>
                {
                    Console.WriteLine("Waiting for request!!");
                    while (true)
                    {
                        Thread.Sleep(1000);
                        var client1 = new HttpClient();
                        client1.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));

                        var response1 = client1.GetAsync(string.Format("http://localhost:63653/api/CallingRequest?id={0}", 1)).Result;
                        var content2 = response1.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(content2);
                        if (content2 != null || content2 != "" || content2 != "[]")
                            return content;
                    }
                }
                );

                getRequest.Start();

                var req = getRequest.Result;

                Console.WriteLine("You have answer!");

                var receiveMessage = new Task(() =>
                {
                    Console.WriteLine("wait");
                    while (true)
                    {
                        Thread.Sleep(1000);
                        var client33 = new HttpClient();
                        client33.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));

                        var response22 = client33.GetAsync(string.Format("http://localhost:63653/api/CallProcess?senderID={0}&receiverID={1}", 8, 1)).Result;
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
                            new KeyValuePair<string, string>("SenderID", "1"),
                            new KeyValuePair<string, string>("ReceiverID", "8"),
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
        private static void MainAsync()
        {
            var identityClient = new DiscoveryClient("http://192.168.88.136:59447"); //discover the IdentityServer
            identityClient.Policy.RequireHttps = false;

            var identityServer = identityClient.GetAsync().Result;

            if (identityServer.IsError)
            {
                Console.Write(identityServer.Error);
                return;
            }

            Console.Write("Enter Username : ");
            var username = Console.ReadLine();
            Console.Write("Enter Password : ");
            var password = Console.ReadLine();

            //Console.WriteLine(identityServer.TokenEndpoint);

            //Get the token
            var tokenClient = new TokenClient(identityServer.TokenEndpoint, "ChatClient", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync(username, password, "UserAPI").Result;
            //Console.WriteLine(tokenResponse.AccessToken);
            //Call the API

            HttpClient client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));

            //UriBuilder builder = new UriBuilder("http://192.168.88.21:61366/api/users");
            //builder.Query = "id=1";


            //Console.WriteLine(builder.Uri);
            var response = client.GetAsync("http://192.168.88.136:61366/api/users/" + username).Result;// client.GetAsync(builder.Uri).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            //var bee = JsonConvert.DeserializeObject(content);
            if(content=="")
                Console.WriteLine("incorrect");
            Console.WriteLine(content);
            Console.ReadKey();
        }

        private static bool SendValidationCode(string receiver)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
            var paramContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", receiver)
            });

            var response = client.PostAsync("http://192.168.88.136:61366/api/Validation", paramContent).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            return bool.Parse(content);
        }

        public static bool Send(string receiver, string code)
        {
            string fromaddr = "whisperscdministration@gmail.com";
            string toaddr = receiver;//TO ADDRESS HERE
            string password = "whisper123.0";

            try
            {
                MailMessage msg = new MailMessage();
                msg.Subject = "Validation code";
                msg.From = new MailAddress(fromaddr);
                msg.Body = string.Format("Your validation code is {0}", code);
                msg.To.Add(new MailAddress(toaddr));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential(fromaddr, password);
                smtp.Credentials = nc;
                smtp.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
