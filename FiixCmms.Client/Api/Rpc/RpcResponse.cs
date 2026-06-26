namespace FiixCmms.Client.Api.Rpc;

/// <summary>
/// Response from an RPC request.
/// </summary>
public class RpcResponse : Response
{
    /// <summary>
    /// The result data from the RPC call.
    /// Can be any type depending on the RPC method called.
    /// </summary>
    public object? Data { get; set; }
}
