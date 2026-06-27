namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetEvent' table.
/// </summary>
public partial class AssetEvent : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateSubmitted { get; set; }
    public long? IntAssetEventTypeID { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntSubmittedByUserID { get; set; }
    public long? IntWorkOrderID { get; set; }
    public string? StrAdditionalDescription { get; set; }
}

