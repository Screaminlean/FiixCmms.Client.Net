namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ScheduledMaintenanceAsset' table.
/// </summary>
public class ScheduledMaintenanceAsset : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntScheduledMaintenanceID { get; set; }
}

