using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Request for adding (creating) a new record.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class AddRequest<T> : Request where T : ClientCmmsDto
{
    public string? ClassName { get; set; }

    /// <summary>
    /// The object to create.
    /// </summary>
    public T? Object { get; set; }

    /// <summary>
    /// Comma-separated list of fields to return in the response.
    /// Example: "id, strName, strCode"
    /// </summary>
    public string? Fields { get; set; }
}
