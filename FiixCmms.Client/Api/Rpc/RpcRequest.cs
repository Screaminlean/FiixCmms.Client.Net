namespace FiixCmms.Client.Api.Rpc;

/// <summary>
/// Base RPC (Remote Procedure Call) request.
/// Used to invoke server-side procedures/methods.
/// </summary>
public class RpcRequest : Request
{
    /// <summary>
    /// The name of the RPC method to call (e.g., "Ping", "CustomFields").
    /// </summary>
    public string? Name { get; set; }
}
