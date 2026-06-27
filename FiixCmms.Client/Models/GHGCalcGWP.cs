namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'GHGCalcGWP' table.
/// </summary>
public partial class GHGCalcGWP : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntHundredYearGWP { get; set; }
    public string? StrDescription { get; set; }
    public string? StrName { get; set; }
}

