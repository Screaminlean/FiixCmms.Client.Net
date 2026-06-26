namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetProperty' table.
/// </summary>
public class AssetProperty : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntAssetPropertyTypeID { get; set; }
    public long? IntMeterReadingUnitID { get; set; }
    public string? StrCode { get; set; }
    public string? StrName { get; set; }
}

