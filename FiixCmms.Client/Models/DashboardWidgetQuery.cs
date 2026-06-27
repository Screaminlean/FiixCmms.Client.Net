namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidgetQuery' table.
/// </summary>
public partial class DashboardWidgetQuery : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntDashboardWidgetID { get; set; }
    public long? IntSubQueryTypeID { get; set; }
    public string? StrLabel { get; set; }
    public string? StrQuery { get; set; }
}

