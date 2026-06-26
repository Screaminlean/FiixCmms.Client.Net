namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetClassificationLog' table.
/// </summary>
public class AssetClassificationLog : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateApplied { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntClassificationID { get; set; }
    public long? IntSiteID { get; set; }
}

