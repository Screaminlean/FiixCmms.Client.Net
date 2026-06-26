namespace FiixCmms.Client.Api;

/// <summary>
/// Base class for all API requests.
/// </summary>
public abstract class Request
{
    public long RequestId { get; set; }
    public string? ClientVersion { get; set; }
    public long RequestSentUnixTime { get; set; }
}
