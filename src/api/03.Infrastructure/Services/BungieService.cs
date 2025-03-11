using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Nodes;
using _03.Infrastructure.Apis;
using _03.Infrastructure.Models.BungieApi.Types;
using Microsoft.Extensions.Caching.Memory;

namespace _03.Infrastructure.Services;

public interface IBungieService
{
    Task<ImmutableDictionary<string, TContent>> GetContentAsync<TContent>(string language, string name) where TContent : BungieApiTypeBase;
}

internal class BungieService : IBungieService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IBungieApi _bungieApi;

    private const string ManifestCacheKey = "bungie.api.destiny.manifest";
    private const string ContentCacheKeyTemplate = "bungie.api.destiny.content.{0}.{1}";
    
    public BungieService(IMemoryCache memoryCache, IBungieApi bungieApi)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _bungieApi = bungieApi ?? throw new ArgumentNullException(nameof(bungieApi));
    }

    public Task<ImmutableDictionary<string, TContent>> GetContentAsync<TContent>(string language, string name) where TContent : BungieApiTypeBase
        => _memoryCache.GetOrCreateAsync(
            GetCacheKey(ContentCacheKeyTemplate, language, name),
            async _ =>
            {
                var manifest = await GetManifest();
                var contentPath = manifest?["Response"]?["jsonWorldComponentContentPaths"]?[language]?[name]?.GetValue<string>();
                if (contentPath is null)
                    throw new InvalidOperationException("Could not extract content path from manifest.");

                var result = await _bungieApi.GetContent(contentPath);
                if (result is null)
                    throw new InvalidOperationException($"Could not retrieve requested content. ContentLanguage = {language}, ContentName = {name}.");

                return result
                    .Where(v => v.Value != null)
                    .ToImmutableDictionary(
                        kvp => kvp.Key, 
                        kvp => JsonSerializer.Deserialize<TContent>(kvp.Value!.ToJsonString()));
            })!;

    private Task<JsonObject?> GetManifest()
        => _memoryCache.GetOrCreateAsync(ManifestCacheKey, _ => _bungieApi.GetManifest());
    
    private string GetCacheKey(string cacheKeyBase, params object?[] cacheKeyParts)
        => string.Format(cacheKeyBase, cacheKeyParts);
}