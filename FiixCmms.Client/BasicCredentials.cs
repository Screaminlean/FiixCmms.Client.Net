namespace FiixCmms.Client;

using FiixCmms.Client.Interfaces;

/// <summary>
/// Basic implementation of credentials for Fiix CMMS API authentication.
/// </summary>
public class BasicCredentials : ICredentials
{
    /// <summary>
    /// Initializes a new instance of the BasicCredentials class.
    /// </summary>
    /// <param name="appKey">The application key for API authentication.</param>
    /// <param name="accessKey">The access key for API authentication.</param>
    /// <param name="secretKey">The secret key for API authentication and request signing.</param>
    public BasicCredentials(string appKey, string accessKey, string secretKey)
    {
        AppKey = appKey ?? throw new ArgumentNullException(nameof(appKey));
        AccessKey = accessKey ?? throw new ArgumentNullException(nameof(accessKey));
        SecretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
    }

    /// <inheritdoc/>
    public string AppKey { get; }

    /// <inheritdoc/>
    public string AccessKey { get; }

    /// <inheritdoc/>
    public string SecretKey { get; }
}
