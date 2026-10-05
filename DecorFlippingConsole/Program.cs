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
    Console.WriteLine(result);

    //Convert string into JSON
    Root deserialized = JsonSerializer.Deserialize<Root>(result);
    Console.WriteLine("Successful Deserialization.");
    Console.WriteLine(deserialized.result.ToString());

    foreach(Realm realm in deserialized.result.realms) Console.WriteLine(realm);


    Console.ReadLine();
}

    public class Realm
    {
        public string product { get; set; }
        public string region { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
    }
    public class Request
    {
        public string list { get; set; }
    }
    public class Result
    {
        public DateTime lastUpdated { get; set; }
        public List<Realm> realms { get; set; }
    }
    public class Root
    {
        public Request request { get; set; }
        public Result result { get; set; }
    }