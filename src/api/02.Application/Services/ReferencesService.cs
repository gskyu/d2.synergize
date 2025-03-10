using System.Text.Json;
using System.Text.Json.Nodes;
using _03.Infrastructure.Services;

namespace _02.Application.Services;

public interface IReferencesService
{
    Task< IEnumerable<KeyValuePair<string,JsonNode?>>?> GetSubClassReferencesAsync(CancellationToken cancellationToken);
}

public class ReferencesService : IReferencesService
{
    private readonly IBungieService _bungieService;

    public ReferencesService(IBungieService bungieService)
    {
        _bungieService = bungieService ?? throw new ArgumentNullException(nameof(bungieService));
    }

    public async Task< IEnumerable<KeyValuePair<string,JsonNode?>>?> GetSubClassReferencesAsync(CancellationToken cancellationToken)
    {
        // TODO: setup standards with shared enums and constants 
        var content = await _bungieService.GetContent("en", "DestinyInventoryItemDefinition");
        var subClasses = content
            .Where(n => n.Value!.GetValueKind() == JsonValueKind.Object)
            .Where(n => n.Value?["itemCategoryHashes"] is JsonArray itemCategoryHashes && itemCategoryHashes.Any(v => (v?.GetValue<long>() ?? 0) == 3109687656));
        return subClasses.ToDictionary();
    }
}