namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'RegionUser' table.
/// </summary>
public class RegionUser : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntRegionID { get; set; }
    public long? IntUserID { get; set; }
}

