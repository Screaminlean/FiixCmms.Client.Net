namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'DashboardPersona' table.
/// </summary>
public class DashboardPersona : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

