using System.Text.Json.Nodes;
using Refit;

namespace _03.Infrastructure.Apis;

// TODO: make it internal and create a service collection extension method to register the services of this project
public interface IBungieApi
{
    [Get("/Platform/Destiny2/Manifest/")]
    Task<JsonObject> GetManifest();
    
    [Get("https://www.bungie.net/{contentPath}")]
    Task<JsonObject> GetContent(string contentPath);
    
    [Get("/Platform/Destiny2/Manifest/DestinyInventoryItemDefinition/{itemHash}")]
    Task<JsonObject> GetInventoryItemDefinition(string itemHash);
}

