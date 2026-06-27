namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetActivityLog' table.
/// </summary>
public partial class AssetActivityLog : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDate { get; set; }
    public long? IntActivityLogID { get; set; }
    public long? IntMoveAssetID { get; set; }
    public long? IntMoveBackAssetID { get; set; }
    public long? IntMoveBackID { get; set; }
    public long? IntMoveID { get; set; }
    public long? IntUserID { get; set; }
}

