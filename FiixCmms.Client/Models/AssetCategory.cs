namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetCategory' table.
/// </summary>
public partial class AssetCategory : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public long? BolOverrideRules { get; set; }
    public long? IntParentID { get; set; }
    public string? StrName { get; set; }
}

