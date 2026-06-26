namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'SiteUser' table.
/// </summary>
public class SiteUser : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntUserID { get; set; }
}

