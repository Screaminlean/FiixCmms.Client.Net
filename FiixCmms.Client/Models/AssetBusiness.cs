namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetBusiness' table.
/// </summary>
public partial class AssetBusiness : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolPreferredVendor { get; set; }
    public long? BolSendRFQs { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntBusinessGroupID { get; set; }
    public long? IntBusinessID { get; set; }
    public long? IntBusinessRoleTypeID { get; set; }
    public double? QtyEconomicBatchQuantity { get; set; }
    public string? StrBusinessAssetNumber { get; set; }
    public string? StrCatalog { get; set; }
}

