using System.Text.Json.Serialization;

namespace FiixCmms.Client.Api.Batch;

/// <summary>
/// Request for batch operations - allows multiple API calls in a single request.
/// Batch requests are transactional: if one request fails, all operations are rolled back.
/// </summary>
public class BatchRequest : Request
{
    /// <summary>
    /// Array of prepared requests to execute in the batch.
    /// Can include any combination of CRUD and RPC requests.
    /// </summary>
    [JsonPropertyName("requests")]
    public List<Request> Requests { get; set; } = new();
}
