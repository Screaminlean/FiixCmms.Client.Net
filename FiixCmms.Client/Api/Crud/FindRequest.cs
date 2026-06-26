using FiixCmms.Client.Models;

namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Request for finding records with optional filters and pagination.
/// </summary>
/// <typeparam name="T">The DTO type.</typeparam>
public class FindRequest<T> : Request where T : ClientCmmsDto
{
    public string? ClassName { get; set; }

    /// <summary>
    /// List of filters to apply to the search.
    /// Each filter uses query language (Ql) with parameters.
    /// </summary>
    public List<FindFilter>? Filters { get; set; }

    /// <summary>
    /// Comma-separated list of fields to return.
    /// Example: "id, strName, strCode, bolIsOnline"
    /// </summary>
    public string? Fields { get; set; }

    /// <summary>
    /// Field name to order results by.
    /// Example: "strName" or "strName DESC"
    /// </summary>
    public string? OrderBy { get; set; }

    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
