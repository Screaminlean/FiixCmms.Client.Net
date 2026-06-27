namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ScheduledMaintenancePart' table.
/// </summary>
public partial class ScheduledMaintenancePart : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntPartID { get; set; }
    public long? IntScheduledMaintenanceID { get; set; }
    public long? IntStockID { get; set; }
    public double? QtySuggestedQuantity { get; set; }
}

