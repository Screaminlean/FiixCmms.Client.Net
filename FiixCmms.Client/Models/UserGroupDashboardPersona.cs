namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'UserGroupDashboardPersona' table.
/// </summary>
public class UserGroupDashboardPersona : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntDashboardPersonaID { get; set; }
    public long? IntUserGroupID { get; set; }
}

