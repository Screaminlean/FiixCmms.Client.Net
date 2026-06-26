using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Response for adding a record.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class AddResponse<T> : Response where T : ClientCmmsDto
{
    /// <summary>
    /// The created object with populated fields (like generated ID).
    /// </summary>
    public T? Object { get; set; }
}
