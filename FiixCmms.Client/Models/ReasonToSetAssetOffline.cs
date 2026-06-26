namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ReasonToSetAssetOffline' table.
/// </summary>
public class ReasonToSetAssetOffline : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

