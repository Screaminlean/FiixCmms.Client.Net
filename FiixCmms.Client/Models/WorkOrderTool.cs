namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderTool' table.
/// </summary>
public class WorkOrderTool : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmUsedFrom { get; set; }
    public DateTime? DtmUsedUntil { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntWorkOrderID { get; set; }
}

