namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'ReceiptLineItem' table.
/// </summary>
public class ReceiptLineItem : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblPurchasePricePerUnit { get; set; }
    public double? DblPurchasePriceTotal { get; set; }
    public DateTime? DtmDateExpiryOfInventoryItems { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntParentReceiptLineItemID { get; set; }
    public long? IntPurchaseOrderLineItemID { get; set; }
    public long? IntReceiptID { get; set; }
    public long? IntReceiveToFacilityID { get; set; }
    public long? IntReceiveToStockID { get; set; }
    public long? IntStockID { get; set; }
    public double? QtyQuantityOrdered { get; set; }
    public double? QtyQuantityReceived { get; set; }
    public string? StrDescription { get; set; }
}

