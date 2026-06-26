namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ChargeDepartment' table.
/// </summary>
public class ChargeDepartment : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntFacilityID { get; set; }
    public string? StrCode { get; set; }
    public string? StrDescription { get; set; }
}

