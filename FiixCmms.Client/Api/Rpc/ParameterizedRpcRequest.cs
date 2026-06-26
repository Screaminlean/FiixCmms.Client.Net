namespace FiixCmms.Client.Api.Rpc;

/// <summary>
/// Parameterized RPC request with action and parameters.
/// Used for RPC methods that require additional action name and parameters.
/// </summary>
public class ParameterizedRpcRequest : Request
{
    /// <summary>
    /// The name of the RPC object (e.g., "CustomFields").
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The action to perform on the RPC object (e.g., "getCustomTableData").
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Parameters to pass to the RPC method.
    /// </summary>
    public Dictionary<string, object>? Parameters { get; set; }
}
