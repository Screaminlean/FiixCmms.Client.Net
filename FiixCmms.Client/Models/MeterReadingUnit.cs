namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MeterReadingUnit' table.
/// </summary>
public partial class MeterReadingUnit : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntPrecision { get; set; }
    public string? StrName { get; set; }
    public string? StrSymbol { get; set; }
}

