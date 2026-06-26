namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Response for removing a record.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class RemoveResponse<T> : Response
{
    /// <summary>
    /// The number of records removed (typically 1 or 0).
    /// </summary>
    public int? Count { get; set; }
}
