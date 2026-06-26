using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Response for finding records by ID.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class FindByIdResponse<T> : Response where T : ClientCmmsDto
{
    public T? Object { get; set; }
}
