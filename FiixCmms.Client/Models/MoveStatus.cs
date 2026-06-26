namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MoveStatus' table.
/// </summary>
public class MoveStatus : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public string? StrDefaultLabel { get; set; }
    public string? StrName { get; set; }
}

