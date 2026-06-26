namespace FiixCmms.Client.Cli;

/// <summary>
/// Configuration settings for Fiix CMMS connection.
/// </summary>
public class FiixCmmsSettings
{
    public string BaseUri { get; set; } = string.Empty;
    public string AppKey { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}
