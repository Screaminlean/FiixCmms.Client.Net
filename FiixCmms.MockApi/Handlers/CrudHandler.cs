using System.Text.Json;
using System.Text.Json.Nodes;
using FiixCmms.MockApi.Store;

namespace FiixCmms.MockApi.Handlers;

/// <summary>
/// Handles Find, FindById, Add, Change, and Remove requests
/// against the in-memory store.
/// </summary>
public static class CrudHandler
{
    private static readonly JsonSerializerOptions WriteOptions = new() { WriteIndented = true };

    // ── Find ──────────────────────────────────────────────────────────────────

    public static string Find(JsonElement body)
    {
        var className = GetString(body, "className") ?? "";
        var fields = ParseFields(GetString(body, "fields"));
        var limit = body.TryGetProperty("limit", out var lp) && lp.TryGetInt32(out var li) ? li : 10;
        var offset = body.TryGetProperty("offset", out var op) && op.TryGetInt32(out var oi) ? oi : 0;
        var filters = ParseFilters(body);

        var table = InMemoryStore.GetTable(className);
        var rows = table.Values.AsEnumerable();

        // Apply simple equality filters derived from Ql patterns
        foreach (var (field, value) in filters)
        {
            rows = rows.Where(r =>
                r.TryGetValue(field, out var el) &&
                el.ToString() == value);
        }

        var allRows = rows.ToList();
        var page = allRows.Skip(offset).Take(limit).ToList();
        var objects = page.Select(r => ProjectFields(r, fields)).ToList();

        var response = new JsonObject
        {
            ["objects"] = JsonArray.Parse(JsonSerializer.Serialize(objects)),
            ["totalObjects"] = allRows.Count
        };

        return response.ToJsonString(WriteOptions);
    }

    // ── FindById ──────────────────────────────────────────────────────────────

    public static string FindById(JsonElement body)
    {
        var className = GetString(body, "className") ?? "";
        var fields = ParseFields(GetString(body, "fields"));
        var id = body.TryGetProperty("id", out var idEl) && idEl.TryGetInt64(out var idVal) ? idVal : -1L;

        var table = InMemoryStore.GetTable(className);

        if (!table.TryGetValue(id, out var record))
        {
            return JsonSerializer.Serialize(new
            {
                error = new { code = 404, message = $"{className} with id {id} not found" }
            }, WriteOptions);
        }

        var projected = ProjectFields(record, fields);
        var response = new JsonObject { ["object"] = JsonNode.Parse(JsonSerializer.Serialize(projected)) };
        return response.ToJsonString(WriteOptions);
    }

    // ── Add ───────────────────────────────────────────────────────────────────

    public static string Add(JsonElement body)
    {
        var className = GetString(body, "className") ?? "";
        var fields = ParseFields(GetString(body, "fields"));
        var table = InMemoryStore.GetTable(className);

        var newRecord = new Dictionary<string, JsonElement>();

        if (body.TryGetProperty("object", out var obj) && obj.ValueKind == JsonValueKind.Object)
        {
            foreach (var prop in obj.EnumerateObject())
                newRecord[prop.Name] = prop.Value.Clone();
        }

        var newId = InMemoryStore.NextId();
        newRecord["id"] = JsonSerializer.SerializeToElement(newId);
        table[newId] = newRecord;

        var projected = ProjectFields(newRecord, fields);
        var response = new JsonObject { ["object"] = JsonNode.Parse(JsonSerializer.Serialize(projected)) };
        return response.ToJsonString(WriteOptions);
    }

    // ── Change ────────────────────────────────────────────────────────────────

    public static string Change(JsonElement body)
    {
        var className = GetString(body, "className") ?? "";
        var fields = ParseFields(GetString(body, "fields"));
        var changeFields = ParseFieldList(GetString(body, "changeFields"));
        var table = InMemoryStore.GetTable(className);

        long id = -1;
        Dictionary<string, JsonElement>? incoming = null;

        if (body.TryGetProperty("object", out var obj) && obj.ValueKind == JsonValueKind.Object)
        {
            incoming = obj.EnumerateObject().ToDictionary(p => p.Name, p => p.Value.Clone());
            if (incoming.TryGetValue("id", out var idEl)) idEl.TryGetInt64(out id);
        }

        if (id == -1 || !table.TryGetValue(id, out var existing))
        {
            return JsonSerializer.Serialize(new
            {
                error = new { code = 404, message = $"{className} with id {id} not found" }
            }, WriteOptions);
        }

        if (incoming != null)
        {
            IEnumerable<string> fieldsToUpdate = changeFields.Count > 0 ? changeFields : incoming.Keys.ToList();
            foreach (var field in fieldsToUpdate)
            {
                if (incoming.TryGetValue(field, out var val))
                    existing[field] = val;
            }
        }

        var projected = ProjectFields(existing, fields);
        var response = new JsonObject { ["object"] = JsonNode.Parse(JsonSerializer.Serialize(projected)) };
        return response.ToJsonString(WriteOptions);
    }

    // ── Remove ────────────────────────────────────────────────────────────────

    public static string Remove(JsonElement body)
    {
        var className = GetString(body, "className") ?? "";
        var id = body.TryGetProperty("id", out var idEl) && idEl.TryGetInt64(out var idVal) ? idVal : -1L;

        var table = InMemoryStore.GetTable(className);

        if (!table.Remove(id))
        {
            return JsonSerializer.Serialize(new
            {
                error = new { code = 404, message = $"{className} with id {id} not found" }
            }, WriteOptions);
        }

        return JsonSerializer.Serialize(new { }, WriteOptions);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static string? GetString(JsonElement el, string key)
    {
        if (el.TryGetProperty(key, out var v) && v.ValueKind == JsonValueKind.String)
            return v.GetString();
        return null;
    }

    private static HashSet<string> ParseFields(string? fields)
    {
        if (string.IsNullOrWhiteSpace(fields)) return new HashSet<string>();
        return fields.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                     .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    private static List<string> ParseFieldList(string? fields)
    {
        if (string.IsNullOrWhiteSpace(fields)) return new List<string>();
        return fields.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
    }

    private static Dictionary<string, JsonElement> ProjectFields(
        Dictionary<string, JsonElement> record, HashSet<string> fields)
    {
        if (fields.Count == 0) return record;

        // Always include "id" and handle display-value prefixed fields (dv_*)
        var result = new Dictionary<string, JsonElement>();
        foreach (var f in fields)
        {
            if (f.StartsWith("dv_", StringComparison.OrdinalIgnoreCase))
            {
                // Return a mock display value string for display-value fields
                var underlying = f[3..]; // strip "dv_"
                result[f] = JsonSerializer.SerializeToElement($"[Mock DV for {underlying}]");
                continue;
            }
            if (record.TryGetValue(f, out var val))
                result[f] = val;
        }
        // Always include id if the record has it
        if (!result.ContainsKey("id") && record.TryGetValue("id", out var idEl))
            result["id"] = idEl;

        return result;
    }

    /// <summary>
    /// Very simple Ql filter parser. Supports patterns like:
    ///   "intSysCode=?"  parameters=[1]
    ///   "intCategoryID=? AND bolIsSite=?"  parameters=[1, 0]
    ///   "strShort=?"  parameters=["CA"]
    /// </summary>
    private static List<(string field, string value)> ParseFilters(JsonElement body)
    {
        var result = new List<(string, string)>();

        if (!body.TryGetProperty("filters", out var filtersEl) ||
            filtersEl.ValueKind != JsonValueKind.Array)
            return result;

        foreach (var filter in filtersEl.EnumerateArray())
        {
            var ql = filter.TryGetProperty("ql", out var qlEl) ? qlEl.GetString() : null;
            if (string.IsNullOrEmpty(ql)) continue;

            List<string> paramValues = new();
            if (filter.TryGetProperty("parameters", out var paramsEl) &&
                paramsEl.ValueKind == JsonValueKind.Array)
            {
                foreach (var p in paramsEl.EnumerateArray())
                    paramValues.Add(p.ToString());
            }

            // Split by AND and extract field=? pairs
            var conditions = ql.Split(new[] { " AND ", " and " }, StringSplitOptions.RemoveEmptyEntries);
            int paramIndex = 0;
            foreach (var cond in conditions)
            {
                var eqIdx = cond.IndexOf('=');
                if (eqIdx < 0) continue;
                var field = cond[..eqIdx].Trim();
                if (paramIndex < paramValues.Count)
                    result.Add((field, paramValues[paramIndex++]));
            }
        }

        return result;
    }
}
