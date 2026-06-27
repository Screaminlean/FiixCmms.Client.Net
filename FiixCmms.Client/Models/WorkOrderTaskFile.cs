namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderTaskFile' table.
/// </summary>
public partial class WorkOrderTaskFile : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntFileID { get; set; }
    public long? IntOrder { get; set; }
    public long? IntWorkOrderTaskID { get; set; }
}

