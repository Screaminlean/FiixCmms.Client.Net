namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'MeterReading' table.
/// </summary>
public partial class MeterReading : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblMeterReading { get; set; }
    public DateTime? DtmDateSubmitted { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntMeterReadingUnitsID { get; set; }
    public long? IntSubmittedByUserID { get; set; }
    public long? IntWorkOrderID { get; set; }
}

