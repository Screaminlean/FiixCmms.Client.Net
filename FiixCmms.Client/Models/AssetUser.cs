namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetUser' table.
/// </summary>
public class AssetUser : ClientCmmsDto
{
    public long? Id { get; set; }
    public DateTime? DtmDateAdded { get; set; }
    public long? IntAssetID { get; set; }
    public long? IntAssetUserTypeID { get; set; }
    public long? IntUserID { get; set; }
}

