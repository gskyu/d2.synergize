using System.Text.Json;
using System.Text.Json.Nodes;
using _03.Infrastructure.Models.BungieApi.Types;
using _03.Infrastructure.Services;

namespace _02.Application.Services;

public interface IReferencesService
{
    Task<IEnumerable<InventoryItemDefinition>> GetSubClassReferencesAsync(CancellationToken cancellationToken);
}

public class ReferencesService : IReferencesService
{
    private readonly IBungieService _bungieService;

    public ReferencesService(IBungieService bungieService)
    {
        _bungieService = bungieService ?? throw new ArgumentNullException(nameof(bungieService));
    }

    public async Task<IEnumerable<InventoryItemDefinition>> GetSubClassReferencesAsync(CancellationToken cancellationToken)
    {
        // TODO: setup standards with shared enums and constants 
        var content = await _bungieService.GetContentAsync<InventoryItemDefinition>("en", "DestinyInventoryItemDefinition");
        var subClasses = content.Values.Where(item => item.Type == 16 && (item.Categories?.Count ?? 0) == 3);
        return subClasses;
    }
}