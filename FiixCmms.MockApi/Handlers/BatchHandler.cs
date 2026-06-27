using System.Text.Json;
using System.Text.Json.Nodes;

namespace FiixCmms.MockApi.Handlers;

/// <summary>
/// Handles BatchRequest by fanning out each sub-request to the appropriate handler.
/// Returns minimal responses containing only the base Response fields (error) so that
/// the abstract Response base class can be deserialized by the client library.
/// </summary>
public static class BatchHandler
{
    private static readonly JsonSerializerOptions WriteOptions = new() { WriteIndented = true };

    public static string Handle(JsonElement body)
    {
        var responses = new JsonArray();

        if (!body.TryGetProperty("requests", out var requestsEl) ||
            requestsEl.ValueKind != JsonValueKind.Array)
        {
            return JsonSerializer.Serialize(new
            {
                error = new { code = -1, message = "BatchRequest missing 'requests' array" }
            }, WriteOptions);
        }

        foreach (var req in requestsEl.EnumerateArray())
        {
            string subResponseJson = DispatchSubRequest(req);

            // Parse the full sub-response but return only the base Response fields
            // (error) so the client can deserialize the abstract Response list.
            JsonNode? errorNode = null;
            try
            {
                var subDoc = JsonDocument.Parse(subResponseJson);
                if (subDoc.RootElement.TryGetProperty("error", out var errEl) &&
                    errEl.ValueKind != JsonValueKind.Null)
                {
                    errorNode = JsonNode.Parse(errEl.GetRawText());
                }
            }
            catch { /* malformed sub-response — leave error null */ }

            var subResponse = new JsonObject();
            if (errorNode != null)
                subResponse["error"] = errorNode;
            else
                subResponse["error"] = null;

            responses.Add(subResponse);
        }

        var batchResponse = new JsonObject { ["responses"] = responses };
        return batchResponse.ToJsonString(WriteOptions);
    }

    private static string DispatchSubRequest(JsonElement req)
    {
        if (req.TryGetProperty("name", out _))
        {
            return req.TryGetProperty("parameters", out _) || req.TryGetProperty("action", out _)
                ? RpcHandler.HandleParameterized(req)
                : RpcHandler.Handle(req);
        }

        if (req.TryGetProperty("object", out var objEl))
        {
            var hasId = objEl.ValueKind == JsonValueKind.Object &&
                        objEl.TryGetProperty("id", out var nestedId) &&
                        nestedId.ValueKind != JsonValueKind.Null;
            return hasId ? CrudHandler.Change(req) : CrudHandler.Add(req);
        }

        if (req.TryGetProperty("id", out var idEl) &&
            idEl.ValueKind != JsonValueKind.Null &&
            !req.TryGetProperty("filters", out _))
        {
            return CrudHandler.FindById(req);
        }

        return CrudHandler.Find(req);
    }
}

