using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

string realmDataRequest = "https://api.undermine.exchange/v1/static/realms.json";

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
Console.WriteLine($"Hello. Your API key is: {config["UndermineExchangeAPIKey"]}");


using HttpClient client = new();
client.DefaultRequestHeaders.Add("Authorization", $"ApiKey {config["UndermineExchangeAPIKey"]}");

//await ProcessRepositoriesAsync(client, realmDataRequest);

static async Task ProcessRepositoriesAsync(HttpClient client, string realmDataRequest)
{
    var json = await client.GetStringAsync(realmDataRequest);
    Console.WriteLine(json);
    Console.ReadLine();
}