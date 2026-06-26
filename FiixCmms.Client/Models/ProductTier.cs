namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ProductTier' table.
/// </summary>
public class ProductTier : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

