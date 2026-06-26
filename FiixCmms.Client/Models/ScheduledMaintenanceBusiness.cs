namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ScheduledMaintenanceBusiness' table.
/// </summary>
public class ScheduledMaintenanceBusiness : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetBusinessID { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntBusinessGroupID { get; set; }
    public long? IntBusinessID { get; set; }
    public long? IntScheduledMaintenanceID { get; set; }
}

