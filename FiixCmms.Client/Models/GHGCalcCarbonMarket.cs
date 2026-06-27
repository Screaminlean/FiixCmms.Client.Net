namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'GHGCalcCarbonMarket' table.
/// </summary>
public partial class GHGCalcCarbonMarket : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblCarbonPrice { get; set; }
    public string? StrJurisdiction { get; set; }
}

