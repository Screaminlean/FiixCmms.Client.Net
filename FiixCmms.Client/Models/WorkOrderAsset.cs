namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderAsset' table.
/// </summary>
public partial class WorkOrderAsset : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntUpdated { get; set; }
    public long? IntWorkOrderID { get; set; }
}

