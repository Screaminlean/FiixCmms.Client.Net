namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'PricingTier' table.
/// </summary>
public partial class PricingTier : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolAvailable { get; set; }
    public double? DblDefaultMonthlyPrice { get; set; }
    public double? DblDefaultPrice { get; set; }
    public long? IntDefaultQuantity { get; set; }
    public long? IntMaximumQuantity { get; set; }
    public long? IntMinimumQuantity { get; set; }
    public long? IntProductID { get; set; }
    public long? IntProductOfferingID { get; set; }
    public long? IntProductTierID { get; set; }
}

