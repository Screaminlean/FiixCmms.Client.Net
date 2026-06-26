namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidgetPositionParameter' table.
/// </summary>
public class DashboardWidgetPositionParameter : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntDashboardWidgetParameterID { get; set; }
    public long? IntWidgetPositionID { get; set; }
    public string? StrParamValue { get; set; }
}

