namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetClassification' table.
/// </summary>
public class AssetClassification : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntClassificationID { get; set; }
    public long? IntSiteID { get; set; }
    public double? QtyAnnualUsage { get; set; }
}

