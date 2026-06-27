namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidgetParameter' table.
/// </summary>
public partial class DashboardWidgetParameter : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolAllowMultipleValues { get; set; }
    public long? IntDashboardWidgetQueryID { get; set; }
    public long? IntParamValueType { get; set; }
    public string? StrDefaultValueQuery { get; set; }
    public string? StrParamEntityQuery { get; set; }
    public string? StrParamEntityType { get; set; }
    public string? StrParamLabel { get; set; }
    public string? StrParamName { get; set; }
}

