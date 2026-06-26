namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'RegionUserGroup' table.
/// </summary>
public class RegionUserGroup : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntGroupID { get; set; }
    public long? IntRegionUserID { get; set; }
}

