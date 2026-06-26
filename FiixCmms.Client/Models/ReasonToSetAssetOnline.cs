namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ReasonToSetAssetOnline' table.
/// </summary>
public class ReasonToSetAssetOnline : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

