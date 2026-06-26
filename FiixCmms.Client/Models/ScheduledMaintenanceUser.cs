namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ScheduledMaintenanceUser' table.
/// </summary>
public class ScheduledMaintenanceUser : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolNotifyOnAssignment { get; set; }
    public long? BolNotifyOnCompletion { get; set; }
    public long? BolNotifyOnOnlineOffline { get; set; }
    public long? BolNotifyOnStatusChange { get; set; }
    public long? BolNotifyOnTaskCompleted { get; set; }
    public long? IntScheduledMaintenanceID { get; set; }
    public long? IntUserID { get; set; }
}

