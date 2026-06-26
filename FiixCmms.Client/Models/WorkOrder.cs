namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'WorkOrder' table.
/// </summary>
public class WorkOrder : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateCompleted { get; set; }
    public DateTime? DtmDateCreated { get; set; }
    public DateTime? DtmDateLastModified { get; set; }
    public DateTime? DtmSuggestedCompletionDate { get; set; }
    public long? IntCompletedByUserID { get; set; }
    public long? IntMaintenanceTypeID { get; set; }
    public long? IntPriorityID { get; set; }
    public long? IntRCAActionID { get; set; }
    public long? IntRCACauseID { get; set; }
    public long? IntRCAProblemID { get; set; }
    public long? IntRequestedByUserID { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntUpdated { get; set; }
    public long? IntWorkOrderStatusGroup { get; set; }
    public long? IntWorkOrderStatusID { get; set; }
    public string? StrAdminNotes { get; set; }
    public string? StrAssetIds { get; set; }
    public string? StrAssets { get; set; }
    public string? StrAssignedUserIds { get; set; }
    public string? StrAssignedUsers { get; set; }
    public string? StrCode { get; set; }
    public string? StrCompletedByUserIds { get; set; }
    public string? StrCompletedByUsers { get; set; }
    public string? StrCompletionNotes { get; set; }
    public string? StrCustomerIds { get; set; }
    public string? StrDescription { get; set; }
    public string? StrEmailUserGuest { get; set; }
    public string? StrNameUserGuest { get; set; }
    public string? StrPhoneUserGuest { get; set; }
    public string? StrVendorIds { get; set; }
}
