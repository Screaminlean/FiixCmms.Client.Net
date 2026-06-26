using System.Security.Cryptography;
using System.Text;
using FiixCmms.Client.Api;
using FiixCmms.Client.Api.Crud;
using FiixCmms.Client.Format;
using FiixCmms.Client.Helpers;
using FiixCmms.Client.Interfaces;
using FiixCmms.Client.Models;
using FiixCmms.Client.Transport;

namespace FiixCmms.Client;

/// <summary>
/// Main client for interacting with the Fiix CMMS API.
/// Provides CRUD operations and handles authentication, request signing, and transport.
/// </summary>
public class FiixCmmsClient
{
    private readonly ICredentials _credentials;
    private ITransport _transport;
    private IFormat _format;
    private long _currentRequestId;
    private readonly string _baseUri;

    /// <summary>
    /// Maximum time in milliseconds to wait when the server indicates throttling (429 status).
    /// Default is 5000ms.
    /// </summary>
    public int MaxWaitOnThrottleMs { get; set; } = 5000;

    /// <summary>
    /// API version sent with requests.
    /// </summary>
    public string ClientVersion { get; set; } = "1.0.0";

    /// <summary>
    /// Initializes a new instance of the FiixCmmsClient class.
    /// </summary>
    /// <param name="credentials">Credentials for API authentication.</param>
    /// <param name="baseUri">Base URI for the API endpoint.</param>
    public FiixCmmsClient(ICredentials credentials, string baseUri)
    {
        _credentials = credentials;
        _baseUri = baseUri;
        _transport = new HttpTransport();
        _format = new JsonFormat();
        _currentRequestId = 0;
    }

    #region CRUD Preparation Methods

    /// <summary>
    /// Prepares a FindById request for the specified DTO type.
    /// </summary>
    public FindByIdRequest<T> PrepareFindById<T>() where T : ClientCmmsDto
    {
        return new FindByIdRequest<T>
        {
            ClassName = typeof(T).Name
        };
    }

    /// <summary>
    /// Prepares a Find request for the specified DTO type.
    /// </summary>
    public FindRequest<T> PrepareFind<T>() where T : ClientCmmsDto
    {
        return new FindRequest<T>
        {
            ClassName = typeof(T).Name
        };
    }

    /// <summary>
    /// Prepares an Add request for the specified DTO type.
    /// </summary>
    public AddRequest<T> PrepareAdd<T>() where T : ClientCmmsDto
    {
        return new AddRequest<T>
        {
            ClassName = typeof(T).Name
        };
    }

    /// <summary>
    /// Prepares a Change request for the specified DTO type.
    /// </summary>
    public ChangeRequest<T> PrepareChange<T>() where T : ClientCmmsDto
    {
        return new ChangeRequest<T>
        {
            ClassName = typeof(T).Name
        };
    }

    /// <summary>
    /// Prepares a Remove request for the specified DTO type.
    /// </summary>
    public RemoveRequest<T> PrepareRemove<T>() where T : ClientCmmsDto
    {
        return new RemoveRequest<T>
        {
            ClassName = typeof(T).Name
        };
    }

    #endregion

    #region CRUD Execution Methods

    /// <summary>
    /// Executes a FindById request.
    /// </summary>
    public async Task<FindByIdResponse<T>> FindByIdAsync<T>(FindByIdRequest<T> request, CancellationToken cancellationToken = default) where T : ClientCmmsDto
    {
        return await ExecuteRequestAsync<FindByIdResponse<T>>(request, cancellationToken);
    }

    /// <summary>
    /// Executes a Find request.
    /// </summary>
    public async Task<FindResponse<T>> FindAsync<T>(FindRequest<T> request, CancellationToken cancellationToken = default) where T : ClientCmmsDto
    {
        return await ExecuteRequestAsync<FindResponse<T>>(request, cancellationToken);
    }

    /// <summary>
    /// Executes an Add request.
    /// </summary>
    public async Task<AddResponse<T>> AddAsync<T>(AddRequest<T> request, CancellationToken cancellationToken = default) where T : ClientCmmsDto
    {
        return await ExecuteRequestAsync<AddResponse<T>>(request, cancellationToken);
    }

    /// <summary>
    /// Executes a Change request.
    /// </summary>
    public async Task<ChangeResponse<T>> ChangeAsync<T>(ChangeRequest<T> request, CancellationToken cancellationToken = default) where T : ClientCmmsDto
    {
        return await ExecuteRequestAsync<ChangeResponse<T>>(request, cancellationToken);
    }

    /// <summary>
    /// Executes a Remove request.
    /// </summary>
    public async Task<RemoveResponse<T>> RemoveAsync<T>(RemoveRequest<T> request, CancellationToken cancellationToken = default) where T : ClientCmmsDto
    {
        return await ExecuteRequestAsync<RemoveResponse<T>>(request, cancellationToken);
    }

    #endregion

    #region RPC Methods

    /// <summary>
    /// Prepares a basic RPC request.
    /// </summary>
    /// <returns>A new RpcRequest instance ready to be configured.</returns>
    public Api.Rpc.RpcRequest PrepareRpc()
    {
        return new Api.Rpc.RpcRequest();
    }

    /// <summary>
    /// Executes a basic RPC request.
    /// </summary>
    /// <param name="request">The RPC request to execute.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The RPC response.</returns>
    public async Task<Api.Rpc.RpcResponse> RpcAsync(Api.Rpc.RpcRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteRequestAsync<Api.Rpc.RpcResponse>(request, cancellationToken);
    }

    /// <summary>
    /// Prepares a parameterized RPC request.
    /// </summary>
    /// <returns>A new ParameterizedRpcRequest instance ready to be configured.</returns>
    public Api.Rpc.ParameterizedRpcRequest PrepareParameterizedRpc()
    {
        return new Api.Rpc.ParameterizedRpcRequest();
    }

    /// <summary>
    /// Executes a parameterized RPC request with strongly-typed response.
    /// </summary>
    /// <typeparam name="T">The expected type of the response data.</typeparam>
    /// <param name="request">The parameterized RPC request to execute.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The parameterized RPC response with strongly-typed data.</returns>
    public async Task<Api.Rpc.ParameterizedRpcResponse<T>> RpcAsync<T>(Api.Rpc.ParameterizedRpcRequest request, CancellationToken cancellationToken = default) where T : class
    {
        return await ExecuteRequestAsync<Api.Rpc.ParameterizedRpcResponse<T>>(request, cancellationToken);
    }

    /// <summary>
    /// Executes a paged RPC request that returns paginated results.
    /// </summary>
    /// <typeparam name="T">The type of items in the paged result.</typeparam>
    /// <param name="request">The RPC request to execute.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The paged response with collection of items.</returns>
    public async Task<Api.Rpc.PagedResponse<T>> RpcPagedAsync<T>(Api.Rpc.RpcRequest request, CancellationToken cancellationToken = default) where T : class
    {
        return await ExecuteRequestAsync<Api.Rpc.PagedResponse<T>>(request, cancellationToken);
    }

    #endregion

    #region Batch Methods

    /// <summary>
    /// Prepares a batch request that can contain multiple CRUD and/or RPC requests.
    /// Batch requests are transactional - if any request fails, all operations are rolled back.
    /// </summary>
    /// <returns>A new BatchRequest instance ready to have requests added to it.</returns>
    public Api.Batch.BatchRequest PrepareBatch()
    {
        return new Api.Batch.BatchRequest();
    }

    /// <summary>
    /// Executes a batch request containing multiple operations.
    /// All requests are executed as a single transaction - if any fails, all are rolled back.
    /// </summary>
    /// <param name="request">The batch request containing multiple operations.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A batch response containing responses for each request in the batch.</returns>
    public async Task<Api.Batch.BatchResponse> BatchAsync(Api.Batch.BatchRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteRequestAsync<Api.Batch.BatchResponse>(request, cancellationToken);
    }

    #endregion

    #region Private Helper Methods

    private async Task<TResponse> ExecuteRequestAsync<TResponse>(Request request, CancellationToken cancellationToken) where TResponse : Response, new()
    {
        try
        {
            request.RequestId = Interlocked.Increment(ref _currentRequestId);
            request.ClientVersion = ClientVersion;
            request.RequestSentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var requestString = _format.RequestToString(request);
            var transportParams = CalculateTransportParameters(request);

            var totalThrottleWaitMs = 0;

            while (true)
            {
                try
                {
                    var responseString = await _transport.SendAsync(
                        transportParams.Uri,
                        transportParams.Headers,
                        requestString,
                        cancellationToken);

                    if (string.IsNullOrEmpty(responseString))
                    {
                        return new TResponse
                        {
                            Error = new ApiError
                            {
                                Leg = "SERVER_REQUEST_RECEIVE",
                                Code = -1,
                                Message = "Empty response from server"
                            }
                        };
                    }

                    var response = _format.StringToResponse<TResponse>(responseString);

                    if (response.Error != null && 
                        response.Error.Code == ApiError.ThrottledClientShouldAutoRetry)
                    {
                        var waitAdvisoryMs = 4000;
                        if (response.Error.Object != null &&
                            response.Error.Object.TryGetValue(ApiError.PClientAutoRetryWaitAdvisoryMs, out var waitValue))
                        {
                            waitAdvisoryMs = Convert.ToInt32(waitValue);
                        }

                        totalThrottleWaitMs += waitAdvisoryMs;
                        if (totalThrottleWaitMs <= MaxWaitOnThrottleMs)
                        {
                            await Task.Delay(waitAdvisoryMs, cancellationToken);
                            continue;
                        }
                    }

                    return response;
                }
                catch (HttpRequestException ex)
                {
                    return new TResponse
                    {
                        Error = new ApiError
                        {
                            Leg = "CLIENT_REQUEST_SEND_OR_RECEIVE",
                            Code = -1,
                            Message = $"HTTP error: {ex.Message}"
                        }
                    };
                }
                catch (TaskCanceledException ex)
                {
                    return new TResponse
                    {
                        Error = new ApiError
                        {
                            Leg = "CLIENT_REQUEST_SEND_OR_RECEIVE",
                            Code = -1,
                            Message = $"Request timeout or cancelled: {ex.Message}"
                        }
                    };
                }
            }
        }
        catch (Exception ex)
        {
            return new TResponse
            {
                Error = new ApiError
                {
                    Leg = "CLIENT_REQUEST_PACK",
                    Code = -1,
                    Message = $"Request serialization error: {ex.Message}"
                }
            };
        }
    }

    private TransportParameters CalculateTransportParameters(Request request)
    {
        var queryString = BuildQueryString(request);
        var uri = $"{_baseUri}?{queryString}";

        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "text/plain" }
        };

        var signature = CalculateSignature(uri);
        headers["Authorization"] = signature;

        return new TransportParameters
        {
            Endpoint = _baseUri,
            QueryString = queryString,
            Uri = uri,
            Headers = headers,
            Signature = signature
        };
    }

    private string BuildQueryString(Request request)
    {
        var sb = new StringBuilder();
        sb.Append($"action={UrlEncodingHelper.EncodeURIComponent(request.GetType().Name)}");
        sb.Append($"&service={UrlEncodingHelper.EncodeURIComponent("cmms")}");
        sb.Append($"&accessKey={UrlEncodingHelper.EncodeURIComponent(_credentials.AccessKey)}");
        sb.Append($"&appKey={UrlEncodingHelper.EncodeURIComponent(_credentials.AppKey)}");
        sb.Append($"&timestamp={UrlEncodingHelper.EncodeURIComponent(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString())}");
        sb.Append("&signatureVersion=1");
        sb.Append("&signatureMethod=HmacSHA256");

        return sb.ToString();
    }

    private string CalculateSignature(string uri)
    {
        var message = uri;

        if (message.StartsWith("http://"))
        {
            message = message.Substring("http://".Length);
        }
        else if (message.StartsWith("https://"))
        {
            message = message.Substring("https://".Length);
        }

        var messageBytes = Encoding.UTF8.GetBytes(message);
        var keyBytes = Encoding.UTF8.GetBytes(_credentials.SecretKey);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(messageBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    #endregion

    #region Properties

    public ITransport Transport
    {
        get => _transport;
        set => _transport = value;
    }

    public IFormat Format
    {
        get => _format;
        set => _format = value;
    }

    public ICredentials Credentials => _credentials;

    public string BaseUri => _baseUri;

    #endregion

    /// <summary>
    /// Internal class for holding transport parameters.
    /// </summary>
    private class TransportParameters
    {
        public string Endpoint { get; set; } = string.Empty;
        public string QueryString { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public Dictionary<string, string> Headers { get; set; } = new();
        public string Signature { get; set; } = string.Empty;
    }
}
