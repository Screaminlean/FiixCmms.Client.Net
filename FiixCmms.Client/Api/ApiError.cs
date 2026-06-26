namespace FiixCmms.Client.Api;

/// <summary>
/// Represents an API error.
/// </summary>
public class ApiError
{
    public string? Leg { get; set; }
    public int Code { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, object>? Object { get; set; }

    public const int ThrottledClientShouldAutoRetry = 429;
    public const string PClientAutoRetryWaitAdvisoryMs = "clientAutoRetryWaitAdvisoryMs";
}
