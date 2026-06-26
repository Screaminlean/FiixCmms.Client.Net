namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'PurchaseOrderLineItem' table.
/// </summary>
public class PurchaseOrderLineItem : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolAddedDirectlyToPurchaseOrder { get; set; }
    public long? BolProductionEquipmentDownWhileOnOrder { get; set; }
    public long? BolSupplierConfirmed { get; set; }
    public double? DblRemoteOrgUnitPrice { get; set; }
    public double? DblTaxRate { get; set; }
    public double? DblTotalPrice { get; set; }
    public double? DblUnitPrice { get; set; }
    public DateTime? DtmDateCreated { get; set; }
    public DateTime? DtmRequiredByDate { get; set; }
    public long? IntAccountID { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntChargeDepartmentID { get; set; }
    public long? IntPurchaseOrderID { get; set; }
    public long? IntRequestedByUserID { get; set; }
    public long? IntShipToLocationID { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntSourceAssetID { get; set; }
    public long? IntSourceWorkOrderID { get; set; }
    public long? IntStockHistoryID { get; set; }
    public long? IntStockID { get; set; }
    public long? IntSupplierID { get; set; }
    public double? QtyOnOrder { get; set; }
    public double? QtyRecieved { get; set; }
    public string? StrBusinessAssetNumber { get; set; }
    public string? StrDescription { get; set; }
    public string? StrJSON { get; set; }
}

