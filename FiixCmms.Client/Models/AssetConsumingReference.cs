namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetConsumingReference' table.
/// </summary>
public partial class AssetConsumingReference : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntBOMControlID { get; set; }
    public long? IntBOMPartControlID { get; set; }
    public long? IntConsumesAssetID { get; set; }
    public double? QtyMaxConsumption { get; set; }
}

