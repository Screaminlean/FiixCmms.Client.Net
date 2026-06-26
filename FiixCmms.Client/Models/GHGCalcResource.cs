namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'GHGCalcResource' table.
/// </summary>
public class GHGCalcResource : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrDescription { get; set; }
    public string? StrLink { get; set; }
    public string? StrName { get; set; }
}

