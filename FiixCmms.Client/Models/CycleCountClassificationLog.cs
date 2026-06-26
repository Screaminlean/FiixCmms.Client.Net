namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'CycleCountClassificationLog' table.
/// </summary>
public class CycleCountClassificationLog : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateApplied { get; set; }
    public long? IntSiteID { get; set; }
}

