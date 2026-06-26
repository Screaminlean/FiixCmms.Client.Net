namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'KpiResult' table.
/// </summary>
public class KpiResult : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolBase { get; set; }
    public double? DblValue { get; set; }
    public DateTime? DtmTime { get; set; }
    public long? IntErrorCode { get; set; }
    public int? IntQueryHash { get; set; }
    public long? IntWidgetPositionID { get; set; }
    public string? StrQuery { get; set; }
}

