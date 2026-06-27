namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetResolved' table.
/// </summary>
public partial class AssetResolved : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAncestorID { get; set; }
    public long? IntDescendantID { get; set; }
}

