namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Product' table.
/// </summary>
public class Product : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

