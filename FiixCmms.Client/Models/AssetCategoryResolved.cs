namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetCategoryResolved' table.
/// </summary>
public class AssetCategoryResolved : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntChildID { get; set; }
    public long? IntParentID { get; set; }
}

