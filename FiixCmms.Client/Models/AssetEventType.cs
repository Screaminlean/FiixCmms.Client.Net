namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetEventType' table.
/// </summary>
public partial class AssetEventType : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrEventCode { get; set; }
    public string? StrEventDescription { get; set; }
    public string? StrEventName { get; set; }
}

