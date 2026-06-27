namespace FiixCmms.Client.Models;

/// <summary>
/// Base class for all CMMS DTOs. Provides support for extra fields
/// returned by the API that may not be explicitly defined in the DTO.
/// </summary>
public abstract partial class ClientCmmsDto
{
    /// <summary>
    /// Gets or sets extra fields that are not explicitly defined in the DTO.
    /// Used for forward compatibility when the API returns fields not yet modeled.
    /// </summary>
    public Dictionary<string, object>? ExtraFields { get; set; }
}
