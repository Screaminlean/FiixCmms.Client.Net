using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Response for changing a record.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class ChangeResponse<T> : Response where T : ClientCmmsDto
{
    /// <summary>
    /// The updated object with current field values.
    /// </summary>
    public T? Object { get; set; }
}
