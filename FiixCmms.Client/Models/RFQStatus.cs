namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'RFQStatus' table.
/// </summary>
public class RFQStatus : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public long? IntControlID { get; set; }
    public string? StrDefaultLabel { get; set; }
    public string? StrName { get; set; }
}

