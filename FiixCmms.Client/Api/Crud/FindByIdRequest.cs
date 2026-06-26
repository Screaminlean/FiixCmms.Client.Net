using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Request for finding a single record by ID.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class FindByIdRequest<T> : Request where T : ClientCmmsDto
{
    public string? ClassName { get; set; }
    public long? Id { get; set; }

    /// <summary>
    /// Comma-separated list of fields to return.
    /// Example: "id, strName, strCode"
    /// Can include display value fields like "dv_intCountryID"
    /// </summary>
    public string? Fields { get; set; }
}
