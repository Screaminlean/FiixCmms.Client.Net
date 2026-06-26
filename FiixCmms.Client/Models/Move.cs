namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Move' table.
/// </summary>
public class Move : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateConfirmed { get; set; }
    public DateTime? DtmDateRejected { get; set; }
    public DateTime? DtmDateRequested { get; set; }
    public DateTime? DtmMoveDate { get; set; }
    public long? IntAssetDestinationID { get; set; }
    public long? IntBusinessDestinationID { get; set; }
    public long? IntConfirmedByID { get; set; }
    public long? IntDestinationTypeID { get; set; }
    public long? IntFromSiteID { get; set; }
    public long? IntMoveStatusID { get; set; }
    public long? IntMovedByID { get; set; }
    public long? IntProjectDestinationID { get; set; }
    public long? IntRejectedByID { get; set; }
    public long? IntRequestedByID { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntUserDestinationID { get; set; }
    public long? IntWorkOrderDestinationID { get; set; }
    public string? StrAisle { get; set; }
    public string? StrBin { get; set; }
    public string? StrNotes { get; set; }
    public string? StrRow { get; set; }
}

