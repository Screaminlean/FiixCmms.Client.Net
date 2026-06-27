namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidgetPositionFilter' table.
/// </summary>
public partial class DashboardWidgetPositionFilter : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolActive { get; set; }
    public long? IntSubQueryID { get; set; }
    public long? IntWidgetPositionID { get; set; }
}

