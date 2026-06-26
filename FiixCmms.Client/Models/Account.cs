namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Account' table.
/// </summary>
public class Account : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrCode { get; set; }
    public string? StrDescription { get; set; }
}
