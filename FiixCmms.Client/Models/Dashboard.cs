namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Dashboard' table.
/// </summary>
public partial class Dashboard : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public long? BolAvailable { get; set; }
    public long? IntDashboardType { get; set; }
    public long? IntUserID { get; set; }
    public string? StrDescription { get; set; }
    public string? StrSelectedSites { get; set; }
    public string? StrTitle { get; set; }
}

