namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetCycleCountClassification' table.
/// </summary>
public partial class AssetCycleCountClassification : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntClassificationID { get; set; }
    public long? IntSiteID { get; set; }
    public double? QtyAnnualUsage { get; set; }
}

