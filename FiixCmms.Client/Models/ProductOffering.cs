namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ProductOffering' table.
/// </summary>
public partial class ProductOffering : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

