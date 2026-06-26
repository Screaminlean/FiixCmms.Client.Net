namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderBusiness' table.
/// </summary>
public class WorkOrderBusiness : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntBusinessGroupID { get; set; }
    public long? IntBusinessID { get; set; }
    public long? IntWorkOrderID { get; set; }
}

