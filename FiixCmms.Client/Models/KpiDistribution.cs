namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'KpiDistribution' table.
/// </summary>
public class KpiDistribution : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblValue { get; set; }
    public long? IntWidgetID { get; set; }
}

