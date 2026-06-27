namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderUser' table.
/// </summary>
public partial class WorkOrderUser : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolNotifyOnAssignment { get; set; }
    public long? BolNotifyOnCompletion { get; set; }
    public long? BolNotifyOnOnlineOffline { get; set; }
    public long? BolNotifyOnStatusChange { get; set; }
    public long? BolNotifyOnTaskCompleted { get; set; }
    public long? IntUserID { get; set; }
    public long? IntWorkOrderID { get; set; }
}

