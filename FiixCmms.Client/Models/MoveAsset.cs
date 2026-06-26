namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MoveAsset' table.
/// </summary>
public class MoveAsset : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolAway { get; set; }
    public long? BolExclude { get; set; }
    public long? BolPending { get; set; }
    public long? BolSetOffline { get; set; }
    public long? BolSetOnline { get; set; }
    public DateTime? DtmDateReturned { get; set; }
    public DateTime? DtmReturnDate { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntMoveID { get; set; }
    public long? IntMovedFromID { get; set; }
    public long? IntReasonOfflineID { get; set; }
    public long? IntReasonOnlineID { get; set; }
    public long? IntSiteID { get; set; }
    public string? StrFromAisle { get; set; }
    public string? StrFromBin { get; set; }
    public string? StrFromRow { get; set; }
    public string? StrNotes { get; set; }
}

