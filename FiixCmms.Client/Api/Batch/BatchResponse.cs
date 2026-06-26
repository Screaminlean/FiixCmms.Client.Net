using System.Text.Json.Serialization;

namespace FiixCmms.Client.Api.Batch;

/// <summary>
/// Response from a batch operation.
/// Contains an array of responses corresponding to each request in the batch.
/// </summary>
public class BatchResponse : Response
{
    /// <summary>
    /// Array of responses corresponding to each request in the batch.
    /// The order matches the order of requests in the BatchRequest.
    /// </summary>
    [JsonPropertyName("responses")]
    public List<Response> Responses { get; set; } = new();
}
