namespace FiixCmms.Client.Api;

/// <summary>
/// Base class for all API responses.
/// </summary>
public abstract class Response
{
    public ApiError? Error { get; set; }
}
