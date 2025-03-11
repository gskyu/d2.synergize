using _03.Infrastructure.Models.BungieApi.Types;
using _03.Infrastructure.Services;
using _99.Standards.Constants;

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
        var content = await _bungieService.GetContentAsync<InventoryItemDefinition>(Languages.English, InventoryItemDefinition.TypeName);
        return content.Values.Where(item => item.Type == 16 && (item.Categories?.Count ?? 0) == 3);
    }
}