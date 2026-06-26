namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'User' table.
/// </summary>
public class User : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolApiApplicationUser { get; set; }
    public long? BolApiManaged { get; set; }
    public long? BolGroup { get; set; }
    public long? BolPushNotificationMessages { get; set; }
    public long? IntLocalizationID { get; set; }
    public long? IntUserStatusID { get; set; }
    public string? StrBusinessIds { get; set; }
    public string? StrEmailAddress { get; set; }
    public string? StrFullName { get; set; }
    public string? StrNotes { get; set; }
    public string? StrOneSignalPlayerIDs { get; set; }
    public string? StrPersonnelCode { get; set; }
    public string? StrPlayerID { get; set; }
    public string? StrPreferences { get; set; }
    public string? StrRequestNotes { get; set; }
    public string? StrTelephone { get; set; }
    public string? StrTelephone2 { get; set; }
    public string? StrUserName { get; set; }
    public string? StrUserTitle { get; set; }
}
