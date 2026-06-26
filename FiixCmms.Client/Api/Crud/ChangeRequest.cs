using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Request for changing (updating) an existing record.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class ChangeRequest<T> : Request where T : ClientCmmsDto
{
    public string? ClassName { get; set; }

    /// <summary>
    /// The object with updated values. Must include the ID field.
    /// </summary>
    public T? Object { get; set; }

    /// <summary>
    /// Comma-separated list of fields to return in the response.
    /// Example: "id, strName, strCode"
    /// </summary>
    public string? Fields { get; set; }

    /// <summary>
    /// Comma-separated list of fields to actually update.
    /// Example: "strName, bolIsOnline, strDescription"
    /// Only these fields will be modified on the server.
    /// </summary>
    public string? ChangeFields { get; set; }
}
