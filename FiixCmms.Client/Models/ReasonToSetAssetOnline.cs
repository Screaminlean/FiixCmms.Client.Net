namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ReasonToSetAssetOnline' table.
/// </summary>
public partial class ReasonToSetAssetOnline : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

