namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetPropertyDomainElement' table.
/// </summary>
public class AssetPropertyDomainElement : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetPropertyID { get; set; }
}

