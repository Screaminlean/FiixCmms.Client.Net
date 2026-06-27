namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'SiteUserGroup' table.
/// </summary>
public partial class SiteUserGroup : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntGroupID { get; set; }
    public long? IntSiteUserID { get; set; }
}

