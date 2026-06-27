namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidgetPosition' table.
/// </summary>
public partial class DashboardWidgetPosition : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolLockAspectRatio { get; set; }
    public long? IntDashboardID { get; set; }
    public long? IntHeight { get; set; }
    public long? IntMinHeight { get; set; }
    public long? IntMinWidth { get; set; }
    public long? IntWidgetID { get; set; }
    public long? IntWidth { get; set; }
    public long? IntXPosition { get; set; }
    public long? IntYPosition { get; set; }
    public string? StrTitleOverride { get; set; }
}

