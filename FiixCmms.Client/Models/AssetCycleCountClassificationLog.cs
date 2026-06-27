namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'AssetCycleCountClassificationLog' table.
/// </summary>
public partial class AssetCycleCountClassificationLog : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntCycleCountClassificationLogID { get; set; }
    public string? StrClassification { get; set; }
}

