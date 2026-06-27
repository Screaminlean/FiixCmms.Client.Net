using System.Text.Json;
using System.Text.Json.Nodes;

namespace FiixCmms.MockApi.Handlers;

/// <summary>
/// Handles RpcRequest and ParameterizedRpcRequest actions.
/// Returns mocked data matching the shapes the CLI examples expect.
/// </summary>
public static class RpcHandler
{
    private static readonly JsonSerializerOptions WriteOptions = new() { WriteIndented = true };

    public static string Handle(JsonElement body)
    {
        var name = GetString(body, "name") ?? "";
        return Dispatch(name, body);
    }

    public static string HandleParameterized(JsonElement body)
    {
        var name = GetString(body, "name") ?? "";
        return Dispatch(name, body);
    }

    private static string Dispatch(string name, JsonElement body) => name switch
    {
        "Ping"                  => Ping(),
        "GetTimezone"           => GetTimezone(),
        "GetAccessibleSites"    => GetAccessibleSites(),
        "CustomFields"          => CustomFields(body),
        "Calendar"              => GenericOk("calendar", new { events = Array.Empty<object>() }),
        "AssetResolved"         => GenericOk("assetResolved", new { objects = Array.Empty<object>(), totalObjects = 0 }),
        "WorkOrderLog"          => GenericOk("workOrderLog", new { objects = Array.Empty<object>() }),
        "FollowOnWorkOrders"    => GenericOk("followOnWorkOrders", new { objects = Array.Empty<object>() }),
        "TaskGroupsToWorkOrder" => GenericOk("taskGroupsToWorkOrder", new { }),
        "StocksReceived"        => GenericOk("stocksReceived", new { objects = Array.Empty<object>() }),
        "ActivityLog"           => GenericOk("activityLog", new { objects = Array.Empty<object>() }),
        "AuditTrail"            => GenericOk("auditTrail", new { objects = Array.Empty<object>() }),
        "DashboardWidget"       => GenericOk("dashboardWidget", new { }),
        "DataExport"            => GenericOk("dataExport", new { }),
        "ScheduleTriggerAssetEvent"    => GenericOk("scheduleTrigger", new { }),
        "ScheduleTriggerCommon"        => GenericOk("scheduleTrigger", new { }),
        "ScheduleTriggerMeterReading"  => GenericOk("scheduleTrigger", new { }),
        "ScheduleTriggerTime"          => GenericOk("scheduleTrigger", new { }),
        _ => JsonSerializer.Serialize(new
        {
            error = new { code = -1, message = $"Unknown RPC method: {name}" }
        }, WriteOptions)
    };

    private static string Ping()
    {
        return JsonSerializer.Serialize(new { data = "pong" }, WriteOptions);
    }

    private static string GetTimezone()
    {
        return JsonSerializer.Serialize(new
        {
            data = new
            {
                id = "America/Toronto",
                displayName = "Eastern Time (ET)",
                rawOffset = -18000000,
                dstOffset = 3600000
            }
        }, WriteOptions);
    }

    private static string GetAccessibleSites()
    {
        return JsonSerializer.Serialize(new
        {
            data = new[]
            {
                new { id = 20, strName = "Main Plant", strCode = "MP-01" },
                new { id = 21, strName = "Warehouse A", strCode = "WH-01" }
            }
        }, WriteOptions);
    }

    private static string CustomFields(JsonElement body)
    {
        var action = GetString(body, "action") ?? "";
        return action switch
        {
            "getWorkOrderCustomFieldsMetaData" => JsonSerializer.Serialize(new
            {
                data = new[]
                {
                    new { id = 1, strName = "Priority Notes", strFieldType = "text" },
                    new { id = 2, strName = "Cost Center", strFieldType = "text" }
                }
            }, WriteOptions),
            "getCustomTableData" => JsonSerializer.Serialize(new
            {
                data = new { rows = Array.Empty<object>(), totalRows = 0 }
            }, WriteOptions),
            _ => JsonSerializer.Serialize(new { data = new { } }, WriteOptions)
        };
    }

    private static string GenericOk(string key, object data)
    {
        var node = new JsonObject { ["data"] = JsonNode.Parse(JsonSerializer.Serialize(data)) };
        return node.ToJsonString(WriteOptions);
    }

    private static string? GetString(JsonElement el, string key)
    {
        if (el.TryGetProperty(key, out var v) && v.ValueKind == JsonValueKind.String)
            return v.GetString();
        return null;
    }
}
