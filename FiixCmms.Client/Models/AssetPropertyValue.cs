namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetPropertyValue' table.
/// </summary>
public class AssetPropertyValue : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblElevation { get; set; }
    public double? DblLatitude { get; set; }
    public double? DblLongitude { get; set; }
    public double? DblValue { get; set; }
    public DateTime? DtmCreateDate { get; set; }
    public DateTime? DtmSourceDate { get; set; }
    public long? IntAssetEventTypeId { get; set; }
    public long? IntAssetPropertyDomainElementID { get; set; }
    public long? IntAssetPropertyID { get; set; }
}

