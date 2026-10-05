using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

string realmDataRequest = "https://api.undermine.exchange/v1/static/realms.json";

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
Console.WriteLine($"Hello. Your API key is: {config["UndermineExchangeAPIKey"]}");

using HttpClient client = new();
client.DefaultRequestHeaders.Add("Authorization", $"ApiKey {config["UndermineExchangeAPIKey"]}");

await ProcessRepositoriesAsync(client, realmDataRequest);

static async Task ProcessRepositoriesAsync(HttpClient client, string realmDataRequest)
{
    var result = await client.GetStringAsync(realmDataRequest);

    //Convert string into JSON
    Root deserialized = JsonSerializer.Deserialize<Root>(result);
    Console.WriteLine("Successful Deserialization.");
    
}

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

    public class Realm
    {
        public string product { get; set; }
        public string region { get; set; }
        public string slug { get; set; }
        public string name { get; set; }

    //Methods
    public override string ToString()
    {
        return $"Name: {name} | Region: {region}";
    }

    }
    public class Request
    {
        public string list { get; set; }
    }
    public class Result
    {
        public DateTime lastUpdated { get; set; }
        public List<Realm> realms { get; set; }

        //Methods

        public List<Realm> USRealms()
        {
            List<Realm> US = [];
            foreach (Realm currentRealm in realms)
            {
                if(currentRealm.region == "us") US.Add(currentRealm);
            }
            return US;
        }
    }
    public class Root
    {
        public Request request { get; set; }
        public Result result { get; set; }
    }