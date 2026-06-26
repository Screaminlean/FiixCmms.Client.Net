namespace FiixCmms.Client.Api.Crud;

/// <summary>
/// Represents a filter for Find requests using query language (Ql) with parameterized queries.
/// </summary>
public class FindFilter
{
    /// <summary>
    /// Query language string with ? placeholders for parameters.
    /// Example: "intCategoryID=? AND bolIsSite=?"
    /// </summary>
    public string? Ql { get; set; }

    /// <summary>
    /// Parameters to substitute into the Ql query in order.
    /// The number of parameters must match the number of ? placeholders in Ql.
    /// </summary>
    public List<object>? Parameters { get; set; }
}
