using System.Text.Json.Nodes;
using Refit;

namespace _03.Infrastructure.Apis;

// TODO: make it internal and create a service collection extension method to register the services of this project
internal interface IBungieApi
{
    [Get("/Platform/Destiny2/Manifest/")]
    Task<JsonObject> GetManifest();

    [Get("/{contentPath}")]
    Task<JsonObject> GetContent(string contentPath);
}

