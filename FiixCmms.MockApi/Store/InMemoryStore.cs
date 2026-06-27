using System.Text.Json;

namespace FiixCmms.MockApi.Store;

/// <summary>
/// In-memory data store for all entity types.
/// Seeded with enough data to exercise every CLI example.
/// </summary>
public static class InMemoryStore
{
    private static long _nextId = 100;

    // entity type name (lowercase) -> id -> record (raw JSON dict)
    public static readonly Dictionary<string, Dictionary<long, Dictionary<string, JsonElement>>> Entities = new();

    static InMemoryStore()
    {
        Seed();
    }

    public static long NextId() => _nextId++;

    private static void Seed()
    {
        // --- AssetCategory ---
        // SysCode 1 = "Locations and Facilities" (used by CrudExample)
        AddEntity("assetcategory", new Dictionary<string, object?>
        {
            ["id"] = 1L,
            ["strName"] = "Locations and Facilities",
            ["intSysCode"] = 1L,
            ["intParentID"] = (object?)null,
            ["bolOverrideRules"] = 0L
        });
        AddEntity("assetcategory", new Dictionary<string, object?>
        {
            ["id"] = 2L,
            ["strName"] = "Equipment",
            ["intSysCode"] = 2L,
            ["intParentID"] = (object?)null,
            ["bolOverrideRules"] = 0L
        });

        // --- Country ---
        // strShort = "CA" (used by CrudExample.FindCountryByShortName)
        AddEntity("country", new Dictionary<string, object?>
        {
            ["id"] = 10L,
            ["strName"] = "Canada",
            ["strShort"] = "CA",
            ["strShort2"] = "CA",
            ["strMid"] = "ca"
        });
        AddEntity("country", new Dictionary<string, object?>
        {
            ["id"] = 11L,
            ["strName"] = "United States",
            ["strShort"] = "US",
            ["strShort2"] = "US",
            ["strMid"] = "us"
        });

        // --- Asset ---
        AddEntity("asset", new Dictionary<string, object?>
        {
            ["id"] = 20L,
            ["strName"] = "Main Plant",
            ["strCode"] = "MP-01",
            ["bolIsOnline"] = 1L,
            ["bolIsSite"] = 1L,
            ["intCategoryID"] = 1L,
            ["intSiteID"] = 20L,
            ["strDescription"] = "Primary manufacturing plant",
            ["intCountryID"] = 10L
        });
        AddEntity("asset", new Dictionary<string, object?>
        {
            ["id"] = 21L,
            ["strName"] = "Warehouse A",
            ["strCode"] = "WH-01",
            ["bolIsOnline"] = 1L,
            ["bolIsSite"] = 0L,
            ["intCategoryID"] = 1L,
            ["intSiteID"] = 20L,
            ["strDescription"] = "Primary warehouse",
            ["intCountryID"] = 10L
        });
        AddEntity("asset", new Dictionary<string, object?>
        {
            ["id"] = 22L,
            ["strName"] = "Pump Station 1",
            ["strCode"] = "PS-01",
            ["bolIsOnline"] = 1L,
            ["bolIsSite"] = 0L,
            ["intCategoryID"] = 2L,
            ["intSiteID"] = 20L,
            ["strDescription"] = "Primary pump station",
            ["intCountryID"] = 11L
        });

        // --- WorkOrder ---
        AddEntity("workorder", new Dictionary<string, object?>
        {
            ["id"] = 30L,
            ["strDescription"] = "Quarterly pump inspection",
            ["intPriorityID"] = 2L,
            ["intSiteID"] = 20L,
            ["intStatusID"] = 1L,
            ["strCode"] = "WO-0001"
        });
        AddEntity("workorder", new Dictionary<string, object?>
        {
            ["id"] = 31L,
            ["strDescription"] = "Emergency HVAC repair",
            ["intPriorityID"] = 1L,
            ["intSiteID"] = 20L,
            ["intStatusID"] = 2L,
            ["strCode"] = "WO-0002"
        });

        // --- User ---
        AddEntity("user", new Dictionary<string, object?>
        {
            ["id"] = 40L,
            ["strFirstName"] = "Alice",
            ["strLastName"] = "Maintenance",
            ["strEmail"] = "alice@example.com",
            ["intSiteID"] = 20L
        });
        AddEntity("user", new Dictionary<string, object?>
        {
            ["id"] = 41L,
            ["strFirstName"] = "Bob",
            ["strLastName"] = "Technician",
            ["strEmail"] = "bob@example.com",
            ["intSiteID"] = 20L
        });
    }

    private static void AddEntity(string typeName, Dictionary<string, object?> fields)
    {
        if (!Entities.TryGetValue(typeName, out var table))
        {
            table = new Dictionary<long, Dictionary<string, JsonElement>>();
            Entities[typeName] = table;
        }

        var record = fields.ToDictionary(
            kv => kv.Key,
            kv => kv.Value == null
                ? JsonDocument.Parse("null").RootElement.Clone()
                : JsonSerializer.SerializeToElement(kv.Value));

        var id = fields.TryGetValue("id", out var idObj) && idObj is long lid ? lid : NextId();
        table[id] = record;
    }

    public static Dictionary<long, Dictionary<string, JsonElement>> GetTable(string typeName)
    {
        var key = typeName.ToLowerInvariant();
        if (!Entities.TryGetValue(key, out var table))
        {
            table = new Dictionary<long, Dictionary<string, JsonElement>>();
            Entities[key] = table;
        }
        return table;
    }
}
