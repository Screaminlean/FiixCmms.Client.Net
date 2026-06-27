namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ScheduledTaskFile' table.
/// </summary>
public partial class ScheduledTaskFile : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntFileID { get; set; }
    public long? IntOrder { get; set; }
    public long? IntScheduledTaskID { get; set; }
}

