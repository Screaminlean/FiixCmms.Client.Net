namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Stock' table.
/// </summary>
public class Stock : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntFacilityID { get; set; }
    public double? QtyMinQty { get; set; }
    public double? QtyOnHand { get; set; }
    public string? StrAisle { get; set; }
    public string? StrBin { get; set; }
    public string? StrRow { get; set; }
}

