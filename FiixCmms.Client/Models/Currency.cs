namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Currency' table.
/// </summary>
public class Currency : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrDescription { get; set; }
    public string? StrISOCode { get; set; }
    public string? StrName { get; set; }
    public string? StrSymbol { get; set; }
}

