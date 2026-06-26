namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrderStatus' table.
/// </summary>
public class WorkOrderStatus : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public long? IntControlID { get; set; }
    public string? StrName { get; set; }
}
