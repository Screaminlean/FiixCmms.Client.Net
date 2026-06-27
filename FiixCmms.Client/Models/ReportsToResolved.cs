namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ReportsToResolved' table.
/// </summary>
public partial class ReportsToResolved : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntChildID { get; set; }
    public long? IntParentID { get; set; }
}

