using FiixCmms.Client.Interfaces;

namespace FiixCmms.Client.Transport;

/// <summary>
/// HTTP transport implementation using HttpClient for sending requests to the Fiix CMMS API.
/// </summary>
public class HttpTransport : ITransport
{
    private readonly HttpClient _httpClient;
    private const int StatusCodeOk = 200;
    private const int StatusCodeTooManyRequests = 429;

    /// <summary>
    /// Initializes a new instance of the HttpTransport class.
    /// </summary>
    public HttpTransport()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Initializes a new instance of the HttpTransport class with a custom HttpClient.
    /// </summary>
    /// <param name="httpClient">The HttpClient instance to use.</param>
    public HttpTransport(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<string> SendAsync(
        string uri, 
        IDictionary<string, string> headers, 
        string requestBody, 
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(requestBody, System.Text.Encoding.UTF8, "text/plain")
        };

        foreach (var header in headers)
        {
            if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        var response = await _httpClient.SendAsync(request, cancellationToken);
        var statusCode = (int)response.StatusCode;

        if (statusCode != StatusCodeOk && statusCode != StatusCodeTooManyRequests)
        {
            throw new HttpRequestException($"HttpClient returned status code {statusCode}");
        }

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseBody;
    }
}
