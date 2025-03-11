using System.Text.Json.Serialization;

namespace _03.Infrastructure.Models.BungieApi.Types;

public record InventoryItemDefinition
{
    [JsonPropertyName("hash")]
    public required long Hash { get; init; }
    
    [JsonPropertyName("itemType")]
    public required int Type { get; init; }
    
    [JsonPropertyName("itemCategoryHashes")]
    public IReadOnlyList<long>? Categories { get; init; }
}