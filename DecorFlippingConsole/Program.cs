using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System;

namespace DecorFlippingConsole
{
    class Program {
        
        private static async Task Main(string[] args)
        {
            //Setting up call authority
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("Authorization", $"ApiKey {config["UndermineExchangeAPIKey"]}");
            
            List<string> inputIDs = GetInputIDs();

        }

        #region Items
        static async void ItemInfo(HttpClient client)
        {
            
        }

        //TODO: Make this cleaner for safe inputs
        static List<string> GetInputIDs()
        {
            List<string> IDs = [];
            Console.WriteLine("Write out itemIDs. Write done when complete.");
            string current = Console.ReadLine();
            do
            {
                IDs.Add(current);
                Console.WriteLine($"Added {current} to list. Next Number: ");
                current = Console.ReadLine();
                
            } while (current != "done");
            Console.WriteLine($"Current List is: {IDs.Count} entries long.");
            return IDs;
        }
        #endregion

        #region Realms
        //Getting all Realms Stuff for easy, cheap testing
        static async void RealmInfo(HttpClient client)
        {
            string realmDataRequest = "https://api.undermine.exchange/v1/static/realms.json";
            var result = await client.GetStringAsync(realmDataRequest);
            //Convert string into JSON
            Root deserialized = JsonSerializer.Deserialize<Root>(result);
            Console.WriteLine("Successful Deserialization.");
            PrintUSRealms(deserialized);
        }
        //More Realms testing
        static void PrintUSRealms(Root deserialized)
        {
            //US Realms specifically
            List<Realm> USRealms = deserialized.result.USRealms();
            foreach (var realm in USRealms)
            {
                Console.WriteLine(realm.ToString());
            }
            Console.ReadLine();
        }
        #endregion Realms
    }
    
}


    