﻿using System.Text.Json.Serialization;
using _99.Standards.Constants;

namespace _03.Infrastructure.Models.BungieApi.Types;

public record InventoryItemDefinition : BungieApiTypeBase
{
    public new static readonly string TypeName = BungieApiTypes.InventoryItemDefinition;
    
    [JsonPropertyName("hash")]
    public required long Hash { get; init; }
    
    [JsonPropertyName("itemType")]
    public required int Type { get; init; }
    
    [JsonPropertyName("itemCategoryHashes")]
    public IReadOnlyList<long>? Categories { get; init; }
}