namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'BusinessClassification' table.
/// </summary>
public partial class BusinessClassification : ClientCmmsDto
{
    public long? Id { get; set; }
    public string? StrName { get; set; }
}

