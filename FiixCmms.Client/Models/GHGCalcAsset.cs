namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'GHGCalcAsset' table.
/// </summary>
public class GHGCalcAsset : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolIsStationary { get; set; }
    public double? DblCO2Total { get; set; }
    public double? DblCarbonTax { get; set; }
    public double? DblDistanceAmount { get; set; }
    public double? DblElectricityAmount { get; set; }
    public double? DblFuelAmount { get; set; }
    public double? DblHeatSteamAmount { get; set; }
    public double? DblResult { get; set; }
    public DateTime? DtmDateEnd { get; set; }
    public DateTime? DtmDateStart { get; set; }
    public string? StrDistanceUnit { get; set; }
    public string? StrElectricityUnit { get; set; }
    public string? StrFuelUnit { get; set; }
    public string? StrHeatSteamUnit { get; set; }
}

