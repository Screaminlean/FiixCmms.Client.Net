namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'StockCycleCount' table.
/// </summary>
public class StockCycleCount : ClientCmmsDto
{
    public long? Id { get; set; }
    public double? DblPrice { get; set; }
    public DateTime? DtmDateCounted { get; set; }
    public long? IntCountedBy { get; set; }
    public long? IntCycleCountID { get; set; }
    public long? IntStockID { get; set; }
    public double? QtyExpected { get; set; }
    public double? QtyStockCount { get; set; }
}

