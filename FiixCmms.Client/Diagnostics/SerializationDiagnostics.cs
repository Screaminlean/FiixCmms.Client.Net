using System.Text.Json;
using FiixCmms.Client.Api.Crud;
using FiixCmms.Client.Format;
using FiixCmms.Client.Models;

namespace FiixCmms.Client.Diagnostics;

/// <summary>
/// Diagnostic tools to verify JSON serialization matches Fiix API expectations.
/// Use these methods to inspect the actual JSON being sent to the API.
/// </summary>
public static class SerializationDiagnostics
{
    /// <summary>
    /// Tests serialization of a WorkOrder DTO and prints the JSON output.
    /// Compare this with Java client output to verify compatibility.
    /// </summary>
    public static void TestWorkOrderSerialization()
    {
        Console.WriteLine("=== WorkOrder DTO Serialization Test ===\n");

        var workOrder = new WorkOrder
        {
            Id = 12345,
            StrCode = "WO-TEST-001",
            StrDescription = "Test work order",
            IntSiteID = 1,
            IntPriorityID = 2,
            IntWorkOrderStatusID = 1,
            DtmDateCreated = DateTime.UtcNow
        };

        var format = new JsonFormat();
        var json = format.ResponseToString(workOrder);

        Console.WriteLine("Serialized JSON:");
        Console.WriteLine(json);
        Console.WriteLine("\nExpected property names:");
        Console.WriteLine("  - id: 12345");
        Console.WriteLine("  - strCode: \"WO-TEST-001\"");
        Console.WriteLine("  - intSiteID: 1  (NOT intSiteId)");
        Console.WriteLine("  - intPriorityID: 2");
    }

    /// <summary>
    /// Tests serialization of a FindByIdRequest and prints the JSON output.
    /// </summary>
    public static void TestFindByIdRequestSerialization()
    {
        Console.WriteLine("\n=== FindByIdRequest Serialization Test ===\n");

        var request = new FindByIdRequest<WorkOrder>
        {
            RequestId = 1,
            ClientVersion = "1.0.0",
            RequestSentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            ClassName = "WorkOrder",
            Id = 12345,
            Fields = "id, strCode, strDescription"
        };

        var format = new JsonFormat();
        var json = format.RequestToString(request);

        Console.WriteLine("Serialized JSON:");
        Console.WriteLine(json);
        Console.WriteLine("\n⚠️  CHECK: Does this match Fiix API documentation?");
        Console.WriteLine("If the API expects a type discriminator field, it will be missing.");
    }

    /// <summary>
    /// Tests the property naming policy directly.
    /// </summary>
    public static void TestPropertyNaming()
    {
        Console.WriteLine("\n=== Property Naming Policy Test ===\n");

        var testCases = new Dictionary<string, string>
        {
            { "StrName", "strName" },
            { "IntSiteID", "intSiteID" },
            { "BolIsOnline", "bolIsOnline" },
            { "DtmDateCreated", "dtmDateCreated" },
            { "QtyStockCount", "qtyStockCount" },
            { "Id", "id" }
        };

        Console.WriteLine("Testing C# property → JSON field naming:");
        foreach (var test in testCases)
        {
            var policy = new FiixPropertyNamingPolicy();
            var result = policy.ConvertName(test.Key);
            var status = result == test.Value ? "✓" : "✗";
            Console.WriteLine($"  {status} {test.Key} → {result} (expected: {test.Value})");
        }
    }

    /// <summary>
    /// Creates a sample request and shows the complete HTTP request details.
    /// </summary>
    public static async Task ShowCompleteRequestExample()
    {
        Console.WriteLine("\n=== Complete HTTP Request Example ===\n");

        // This won't actually send, just show what would be sent
        var credentials = new BasicCredentials(
            appKey: "YOUR-APP-KEY",
            accessKey: "YOUR-ACCESS-KEY",
            secretKey: "YOUR-SECRET-KEY"
        );

        var client = new FiixCmmsClient(credentials, "https://your-instance.fiixlabs.com/api");

        var request = client.PrepareFindById<WorkOrder>();
        request.Id = 12345;

        Console.WriteLine("URL would be:");
        Console.WriteLine($"  {client.BaseUri}?action=FindByIdRequest&service=cmms&accessKey=...");
        Console.WriteLine("\nHeaders would include:");
        Console.WriteLine("  Content-Type: text/plain");
        Console.WriteLine("  Authorization: [HMAC SHA256 signature]");
        Console.WriteLine("\nBody would be:");

        var format = new JsonFormat();
        var json = format.RequestToString(request);
        Console.WriteLine(json);

        Console.WriteLine("\n⚠️  To verify against Java client:");
        Console.WriteLine("1. Run the Java client with identical parameters");
        Console.WriteLine("2. Capture the HTTP request (use Fiddler/Charles Proxy)");
        Console.WriteLine("3. Compare JSON bodies for any differences");
    }

    /// <summary>
    /// Helper method to compare two JSON strings and highlight differences.
    /// </summary>
    public static void CompareJson(string dotNetJson, string javaJson)
    {
        Console.WriteLine("\n=== JSON Comparison ===\n");

        try
        {
            var dotNetDoc = JsonDocument.Parse(dotNetJson);
            var javaDoc = JsonDocument.Parse(javaJson);

            Console.WriteLine(".NET JSON properties:");
            PrintJsonProperties(dotNetDoc.RootElement, "  ");

            Console.WriteLine("\nJava JSON properties:");
            PrintJsonProperties(javaDoc.RootElement, "  ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
        }
    }

    private static void PrintJsonProperties(JsonElement element, string indent)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                Console.WriteLine($"{indent}{property.Name}: {property.Value.ValueKind}");
                if (property.Value.ValueKind == JsonValueKind.Object || 
                    property.Value.ValueKind == JsonValueKind.Array)
                {
                    PrintJsonProperties(property.Value, indent + "  ");
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            int index = 0;
            foreach (var item in element.EnumerateArray())
            {
                Console.WriteLine($"{indent}[{index}]: {item.ValueKind}");
                if (item.ValueKind == JsonValueKind.Object || 
                    item.ValueKind == JsonValueKind.Array)
                {
                    PrintJsonProperties(item, indent + "  ");
                }
                index++;
            }
        }
    }
}

// Make the naming policy public for diagnostic access
public class FiixPropertyNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length == 0)
            return name;

        return char.ToLowerInvariant(name[0]) + name.Substring(1);
    }
}
