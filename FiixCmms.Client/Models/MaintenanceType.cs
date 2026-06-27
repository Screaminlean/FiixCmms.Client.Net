namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MaintenanceType' table.
/// </summary>
public partial class MaintenanceType : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public string? StrColor { get; set; }
    public string? StrName { get; set; }
}
