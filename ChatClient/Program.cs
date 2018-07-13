using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync();
        }
        private static void MainAsync()
        {
            var identityServer = DiscoveryClient.GetAsync("http://localhost:59447").Result; //discover the IdentityServer

            if (identityServer.IsError)
            {
                Console.Write(identityServer.Error);
                return;
            }

            Console.Write("Enter Username : ");
            var username = Console.ReadLine();
            Console.Write("Enter Password : ");
            var password = Console.ReadLine();

            //Get the token
            var tokenClient = new TokenClient(identityServer.TokenEndpoint, "ChatClient", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync(username, password, "UserAPI").Result;

            //Call the API

            HttpClient client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
            

            var response = client.GetAsync("http://localhost:61366/api/users").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var bee = JArray.Parse(content);//JsonConvert.DeserializeObject(content);
            Console.WriteLine(bee);
            Console.ReadKey();
        }
    }
}
