namespace FiixCmms.Client.Interfaces;

/// <summary>
/// Represents credentials used to authenticate with the Fiix CMMS API.
/// </summary>
public interface ICredentials
{
    /// <summary>
    /// Gets the access key for API authentication.
    /// </summary>
    string AccessKey { get; }

    /// <summary>
    /// Gets the application key for API authentication.
    /// </summary>
    string AppKey { get; }

    /// <summary>
    /// Gets the secret key for API authentication and request signing.
    /// </summary>
    string SecretKey { get; }
}
