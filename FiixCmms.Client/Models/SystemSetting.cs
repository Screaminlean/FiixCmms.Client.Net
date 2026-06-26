namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'SystemSetting' table.
/// </summary>
public class SystemSetting : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolEnableAllowNegativeStocks { get; set; }
    public long? BolEnableBlockWhenOutOfStock { get; set; }
    public long? BolEnablePromptWhenOutOfStock { get; set; }
    public long? BolRequiredContactInfo { get; set; }
    public string? StrShowContactInfoInputs { get; set; }
}

