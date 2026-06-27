namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderPart' table.
/// </summary>
public partial class WorkOrderPart : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntPartID { get; set; }
    public long? IntStockID { get; set; }
    public long? IntUpdated { get; set; }
    public long? IntWorkOrderID { get; set; }
    public double? QtyActualQuantityUsed { get; set; }
    public double? QtySuggestedQuantity { get; set; }
}

