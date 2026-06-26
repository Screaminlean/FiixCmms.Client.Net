namespace FiixCmms.Client.Api.Rpc;

/// <summary>
/// Paged response from an RPC request that returns paginated data.
/// </summary>
/// <typeparam name="T">The type of items in the page.</typeparam>
public class PagedResponse<T> : Response where T : class
{
    /// <summary>
    /// The collection of items in the current page.
    /// </summary>
    public List<T>? Objects { get; set; }

    /// <summary>
    /// Total number of objects available across all pages.
    /// </summary>
    public int? TotalObjects { get; set; }

    /// <summary>
    /// The current page number (0-based or 1-based depending on API).
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int? PageSize { get; set; }
}
