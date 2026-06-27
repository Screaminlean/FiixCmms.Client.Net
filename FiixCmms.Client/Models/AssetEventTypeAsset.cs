namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetEventTypeAsset' table.
/// </summary>
public partial class AssetEventTypeAsset : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetEventTypeID { get; set; }
    public long? IntAssetID { get; set; }
}

