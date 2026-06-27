namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MoveSiteManager' table.
/// </summary>
public partial class MoveSiteManager : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntUserID { get; set; }
}

