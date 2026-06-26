namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'GHGCalcFuel' table.
/// </summary>
public class GHGCalcFuel : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolIsStationary { get; set; }
    public double? DblGramsCHFourPerPrimaryUnit { get; set; }
    public double? DblGramsCHFourPerSecondaryUnit { get; set; }
    public double? DblGramsNTwoOPerPrimaryUnit { get; set; }
    public double? DblGramsNTwoOPerSecondaryUnit { get; set; }
    public double? DblKgCOTwoPerPrimaryUnit { get; set; }
    public double? DblKgCOTwoPerSecondaryUnit { get; set; }
    public string? StrName { get; set; }
    public string? StrPrimaryUnit { get; set; }
    public string? StrSecondaryUnit { get; set; }
}

