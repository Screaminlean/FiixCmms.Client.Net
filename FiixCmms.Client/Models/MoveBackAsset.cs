namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MoveBackAsset' table.
/// </summary>
public class MoveBackAsset : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolExclude { get; set; }
    public long? BolPending { get; set; }
    public long? BolSetBackOffline { get; set; }
    public long? BolSetBackOnline { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntMoveBackID { get; set; }
    public long? IntOriginalMoveAssetID { get; set; }
    public long? IntReasonOfflineID { get; set; }
    public long? IntReasonOnlineID { get; set; }
    public long? IntSiteID { get; set; }
    public string? StrNotes { get; set; }
    public string? StrToAisle { get; set; }
    public string? StrToBin { get; set; }
    public string? StrToRow { get; set; }
}

