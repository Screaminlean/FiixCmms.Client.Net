namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'Asset' table.
/// </summary>
public partial class Asset : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolIsBillToFacility { get; set; }
    public long? BolIsOnline { get; set; }
    public long? BolIsRegion { get; set; }
    public long? BolIsShippingOrReceivingFacility { get; set; }
    public long? BolIsSite { get; set; }
    public double? DblLastPrice { get; set; }
    public double? DblLatitude { get; set; }
    public double? DblLongitude { get; set; }
    public long? IntAssetLocationID { get; set; }
    public long? IntAssetParentID { get; set; }
    public long? IntCategoryID { get; set; }
    public long? IntCountryID { get; set; }
    public long? IntKind { get; set; }
    public long? IntSiteID { get; set; }
    public long? IntSuperCategorySysCode { get; set; }
    public long? IntUpdated { get; set; }
    public double? QtyMinStockCount { get; set; }
    public double? QtyStockCount { get; set; }
    public string? StrAddress { get; set; }
    public string? StrAisle { get; set; }
    public string? StrBarcode { get; set; }
    public string? StrBinNumber { get; set; }
    public string? StrCity { get; set; }
    public string? StrCode { get; set; }
    public string? StrCustomerIds { get; set; }
    public string? StrDescription { get; set; }
    public string? StrInventoryCode { get; set; }
    public string? StrMASourceProduct { get; set; }
    public string? StrMake { get; set; }
    public string? StrModel { get; set; }
    public string? StrName { get; set; }
    public string? StrNotes { get; set; }
    public string? StrPostalCode { get; set; }
    public string? StrProvince { get; set; }
    public string? StrQuotingTerms { get; set; }
    public string? StrRow { get; set; }
    public string? StrSerialNumber { get; set; }
    public string? StrShippingTerms { get; set; }
    public string? StrStockLocation { get; set; }
    public string? StrUnspcCode { get; set; }
    public string? StrVendorIds { get; set; }
}
