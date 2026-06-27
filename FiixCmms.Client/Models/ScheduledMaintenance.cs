namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ScheduledMaintenance' table.
/// </summary>
public partial class ScheduledMaintenance : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblSuggestedTime { get; set; }
    public DateTime? DtmCreateDate { get; set; }
    public DateTime? DtmUpdatedDate { get; set; }
    public long? IntMaintenanceTypeID { get; set; }
    public long? IntPriorityID { get; set; }
    public long? IntProjectID { get; set; }
    public long? IntRequestorUserID { get; set; }
    public long? IntScheduledMaintenanceStatusID { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntStartAsWorkOrderStatusID { get; set; }
    public long? IntSuggestedCompletion { get; set; }
    public string? StrCode { get; set; }
    public string? StrCompletionNotes { get; set; }
    public string? StrCustomerIds { get; set; }
    public string? StrDescription { get; set; }
    public string? StrVendorIds { get; set; }
}

