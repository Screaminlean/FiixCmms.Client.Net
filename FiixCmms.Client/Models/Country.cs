namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Country' table.
/// </summary>
public partial class Country : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrMid { get; set; }
    public string? StrName { get; set; }
    public string? StrShort { get; set; }
    public string? StrShort2 { get; set; }
}

