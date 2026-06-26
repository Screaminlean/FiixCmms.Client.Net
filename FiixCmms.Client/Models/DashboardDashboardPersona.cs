namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardDashboardPersona' table.
/// </summary>
public class DashboardDashboardPersona : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntDashboardID { get; set; }
    public long? IntDashboardPersonaID { get; set; }
}

