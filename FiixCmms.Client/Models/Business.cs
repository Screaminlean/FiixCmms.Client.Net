namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Business' table.
/// </summary>
public class Business : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntCountryID { get; set; }
    public string? StrAddress { get; set; }
    public string? StrCity { get; set; }
    public string? StrCode { get; set; }
    public string? StrFax { get; set; }
    public string? StrName { get; set; }
    public string? StrNotes { get; set; }
    public string? StrPhone { get; set; }
    public string? StrPhone2 { get; set; }
    public string? StrPostalCode { get; set; }
    public string? StrPrimaryContact { get; set; }
    public string? StrPrimaryEmail { get; set; }
    public string? StrProvince { get; set; }
    public string? StrSecondaryEmail { get; set; }
    public string? StrTimezone { get; set; }
}

