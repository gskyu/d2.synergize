
using _99.Standards.Constants;

namespace _03.Infrastructure.Models.BungieApi.Types;

public abstract record BungieApiTypeBase
{
#pragma warning disable CS0414 // Field is assigned but its value is never used
    public static readonly string TypeName = Utils.Undefined;
#pragma warning restore CS0414 // Field is assigned but its value is never used
}