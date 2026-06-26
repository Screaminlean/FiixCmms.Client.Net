using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Response for finding records.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class FindResponse<T> : Response where T : ClientCmmsDto
{
    public List<T>? Objects { get; set; }
    public int? TotalObjects { get; set; }
}
