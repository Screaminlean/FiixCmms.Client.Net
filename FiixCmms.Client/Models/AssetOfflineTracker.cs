namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetOfflineTracker' table.
/// </summary>
public partial class AssetOfflineTracker : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblProductionHoursAffected { get; set; }
    public DateTime? DtmOffLineTo { get; set; }
    public DateTime? DtmOfflineFrom { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntReasonOfflineID { get; set; }
    public long? IntReasonOnlineID { get; set; }
    public long? IntSetOfflineByUserID { get; set; }
    public long? IntSetOnlineByUserID { get; set; }
    public long? IntWorkOrderID { get; set; }
    public string? StrOfflineAdditionalInfo { get; set; }
    public string? StrOnlineAdditionalInfo { get; set; }
}

