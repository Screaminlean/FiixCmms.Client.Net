namespace FiixCmms.Client.Interfaces;

/// <summary>
/// Interface for transport mechanisms that send requests to the Fiix CMMS API.
/// </summary>
public interface ITransport
{
    /// <summary>
    /// Sends a request to the API and returns the response as a string.
    /// </summary>
    /// <param name="uri">The full URI including query string.</param>
    /// <param name="headers">The HTTP headers to include in the request.</param>
    /// <param name="requestBody">The request body content.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>The response body as a string.</returns>
    Task<string> SendAsync(string uri, IDictionary<string, string> headers, string requestBody, CancellationToken cancellationToken = default);
}
