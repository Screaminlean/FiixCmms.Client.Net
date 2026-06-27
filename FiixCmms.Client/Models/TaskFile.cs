namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'TaskFile' table.
/// </summary>
public partial class TaskFile : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntFileID { get; set; }
    public long? IntOrder { get; set; }
    public long? IntTaskID { get; set; }
}

