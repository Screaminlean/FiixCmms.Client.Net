namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'GHGCalcVehicle' table.
/// </summary>
public class GHGCalcVehicle : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolIsOnRoad { get; set; }
    public double? DblGramsCHFourPerPrimaryUnit { get; set; }
    public double? DblGramsNTwoOPerPrimaryUnit { get; set; }
    public string? StrName { get; set; }
    public string? StrSubNameLabel { get; set; }
}

