namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetPropertyType' table.
/// </summary>
public class AssetPropertyType : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

