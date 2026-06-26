namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'CalendarEvent' table.
/// </summary>
public class CalendarEvent : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDate { get; set; }
    public long? IntScheduleTriggerID { get; set; }
    public long? IntScheduledMaintenanceID { get; set; }
}

