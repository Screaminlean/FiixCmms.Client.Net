namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardWidget' table.
/// </summary>
public class DashboardWidget : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public long? BolAvailable { get; set; }
    public long? BolAvailableRestricted { get; set; }
    public long? BolDisplayAsPercentage { get; set; }
    public long? IntQueryTypeID { get; set; }
    public int? IntResultPrecision { get; set; }
    public int? IntUnitType { get; set; }
    public long? IntWidgetCategoryID { get; set; }
    public string? StrChartType { get; set; }
    public string? StrColorScheme { get; set; }
    public string? StrDescription { get; set; }
    public string? StrName { get; set; }
    public string? StrUnit { get; set; }
}

