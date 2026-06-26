namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ScheduledTask' table.
/// </summary>
public class ScheduledTask : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblTimeEstimatedHours { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntAssignedToUserID { get; set; }
    public long? IntMeterReadingUnitID { get; set; }
    public long? IntOrder { get; set; }
    public long? IntParentScheduledTaskID { get; set; }
    public long? IntScheduledMaintenanceID { get; set; }
    public long? IntTaskType { get; set; }
    public string? StrDescription { get; set; }
}

