namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'PurchaseOrder' table.
/// </summary>
public partial class PurchaseOrder : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntPurchaseOrderStatusID { get; set; }
    public long? IntSiteID { get; set; }
}

