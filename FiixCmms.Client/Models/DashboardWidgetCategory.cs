namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidgetCategory' table.
/// </summary>
public partial class DashboardWidgetCategory : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

