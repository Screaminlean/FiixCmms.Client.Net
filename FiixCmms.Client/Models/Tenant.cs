namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Tenant' table.
/// </summary>
public class Tenant : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolOptGuestRequestor { get; set; }
}

