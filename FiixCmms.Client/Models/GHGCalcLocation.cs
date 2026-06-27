namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'GHGCalcLocation' table.
/// </summary>
public partial class GHGCalcLocation : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblGramsPerkWh { get; set; }
    public string? StrName { get; set; }
    public string? StrRegionLabel { get; set; }
    public string? StrZIPPostalCode { get; set; }
}

