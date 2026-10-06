using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;
using DFCI = DecorFlippingConsoleItems;
using DFCS = DecorFlippingConsoleShopping;

namespace DecorFlippingConsole
{
    class Program {
        
        public static Dictionary<string,string> DecorNamesandIDs = new Dictionary<string,string>();
        public static DFCS.FullShoppingList fullList = new DFCS.FullShoppingList();
        private static async Task Main(string[] args)
        {
            InitalizeNamesAndIDs();
            
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
            foreach (var id in inputIDs)
            {
                string itemDataRequest = $"https://api.undermine.exchange/v1/region/us/items/{id}/now.json";
                var result = await client.GetStringAsync(itemDataRequest);
                DFCI.Root root = JsonSerializer.Deserialize<DFCI.Root>(result);
                List<DFCI.Result> realms = root.orderbyPrice();
                ShoppingListAddition(realms[0], id);
            }
            Console.WriteLine(fullList);
        }

        static void ShoppingListAddition(DFCI.Result cheapestRealm, string itemID)
        {
            string realmName = cheapestRealm.realms[0];
            string itemName = LookUpNameByID(itemID);
            long realPrice = cheapestRealm.price / 10000;
            DFCS.ShoppingItem item = new DFCS.ShoppingItem(itemName, itemID, realPrice);
            fullList.AddItemToFullList(realmName, item);
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
        
        #region Dictionary Setup
        private static void InitalizeNamesAndIDs()
        {
            //Read DecorNamesAndIDs.ods until we find the string with the id in it.
            using (StreamReader stream = new StreamReader("./DecorNamesAndIDs.csv"))
            {
                string line;
                while((line = stream.ReadLine()) != null)
                {
                    string[] split = line.Split(",");
                    //Key should be ID, Value should be Name. I typed the doc backwards.
                    DecorNamesandIDs.Add(split[1], split[0]);
                }
            }
        }

        public static string LookUpNameByID(string num)
        {
            return DecorNamesandIDs.GetValueOrDefault(num);
        }
        #endregion
    }
    
}


    