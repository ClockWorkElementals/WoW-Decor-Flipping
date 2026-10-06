using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System;
using DFCI = DecorFlippingConsoleItems;

namespace DecorFlippingConsole
{
    class Program {
        
        private static async Task Main(string[] args)
        {
            //Setting up call authority
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("Authorization", $"ApiKey {config["UndermineExchangeAPIKey"]}");
            //await RealmInfo(client);
            await ItemInfo(client);

        }

        #region Items
        static async Task ItemInfo(HttpClient client)
        {
            List<string> inputIDs = GetInputIDs();
            Console.WriteLine($"Testing, inputIDs is still {inputIDs.Count} entries long.");
            foreach (var id in inputIDs)
            {
                Console.WriteLine($"current id is {id}");
                string itemDataRequest = $"https://api.undermine.exchange/v1/region/us/items/{id}/now.json";
                var result = await client.GetStringAsync(itemDataRequest);
                DFCI.Root deserialized = JsonSerializer.Deserialize<DFCI.Root>(result);
                PrintRealmsByPrice(deserialized);
                Console.WriteLine("-----------------------------------------------------");
            }
        }
        //TODO: Prolly just kinda remove this and redo entirely.
        static void PrintRealmsByPrice(DFCI.Root root)
        {
            List<DFCI.Result> realms = root.orderbyPrice();
            Console.WriteLine(realms[0].ToString());

            List<string> myRealms = ["aggramar", "aerie-peak", "moon-guard", "wyrmrest-accord", "area-52"];
            foreach (var realm in realms)
            {
                foreach (var myRealm in myRealms)
                {
                    if(realm.realms.Contains(myRealm))
                        Console.WriteLine(realm.ToString());
                    
                }
            }
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
        static async Task RealmInfo(HttpClient client)
        {
            string realmDataRequest = "https://api.undermine.exchange/v1/static/realms.json";
            var result = await client.GetStringAsync(realmDataRequest);
            //Convert string into JSON
            Root deserialized = JsonSerializer.Deserialize<Root>(result);
            Console.WriteLine("Successful Deserialization.");
            PrintUSRealms(deserialized);
            return;
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


    