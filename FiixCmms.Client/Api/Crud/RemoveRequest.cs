using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Request for removing (deleting) a record.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class RemoveRequest<T> : Request where T : ClientCmmsDto
{
    public string? ClassName { get; set; }

    /// <summary>
    /// The ID of the record to remove.
    /// </summary>
    public long? Id { get; set; }
}
