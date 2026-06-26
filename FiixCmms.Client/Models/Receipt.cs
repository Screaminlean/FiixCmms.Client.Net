namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Receipt' table.
/// </summary>
public class Receipt : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateOrdered { get; set; }
    public DateTime? DtmDateReceived { get; set; }
    public long? IntCode { get; set; }
    public long? IntPurchaseCurrencyID { get; set; }
    public long? IntPurchaseOrderID { get; set; }
    public long? IntReceiptStatusID { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntSupplierID { get; set; }
}

