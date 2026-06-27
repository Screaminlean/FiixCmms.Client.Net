namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MoveBack' table.
/// </summary>
public partial class MoveBack : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateCanceled { get; set; }
    public DateTime? DtmDateConfirmed { get; set; }
    public DateTime? DtmDateCreated { get; set; }
    public DateTime? DtmDateRequested { get; set; }
    public DateTime? DtmMoveBackDate { get; set; }
    public long? IntConfirmedByID { get; set; }
    public long? IntFromSiteID { get; set; }
    public long? IntMoveStatusID { get; set; }
    public long? IntMovedBackByUserID { get; set; }
    public long? IntRejectedByID { get; set; }
    public long? IntRequestedByID { get; set; }
    public long? IntSiteID { get; set; }
    public string? StrNotes { get; set; }
}

