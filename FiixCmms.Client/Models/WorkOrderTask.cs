namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderTask' table.
/// </summary>
public partial class WorkOrderTask : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblTimeEstimatedHours { get; set; }
    public double? DblTimeSpentHours { get; set; }
    public DateTime? DtmDateCompleted { get; set; }
    public DateTime? DtmStartDate { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntAssignedToUserID { get; set; }
    public long? IntCompletedByUserID { get; set; }
    public long? IntMeterReadingUnitID { get; set; }
    public long? IntOrder { get; set; }
    public long? IntParentWorkOrderTaskID { get; set; }
    public long? IntTaskGroupControlID { get; set; }
    public long? IntTaskType { get; set; }
    public long? IntUpdated { get; set; }
    public long? IntWorkOrderID { get; set; }
    public string? StrDescription { get; set; }
    public string? StrResult { get; set; }
    public string? StrTaskNotesCompletion { get; set; }
}

