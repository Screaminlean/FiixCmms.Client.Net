namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Priority' table.
/// </summary>
public class Priority : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public long? IntOrder { get; set; }
    public string? StrName { get; set; }
}
