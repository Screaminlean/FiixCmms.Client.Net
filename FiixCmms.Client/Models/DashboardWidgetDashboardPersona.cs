namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidgetDashboardPersona' table.
/// </summary>
public partial class DashboardWidgetDashboardPersona : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntDashboardPersonaID { get; set; }
    public long? IntWidgetID { get; set; }
}

