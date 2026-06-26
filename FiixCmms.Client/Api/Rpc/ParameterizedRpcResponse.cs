namespace FiixCmms.Client.Api.Rpc;

/// <summary>
/// Response from a parameterized RPC request with strongly-typed result.
/// </summary>
/// <typeparam name="T">The type of the result data.</typeparam>
public class ParameterizedRpcResponse<T> : Response where T : class
{
    /// <summary>
    /// The result data from the RPC call.
    /// </summary>
    public T? Data { get; set; }
}
