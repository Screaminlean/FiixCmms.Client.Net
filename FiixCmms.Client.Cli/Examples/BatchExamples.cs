using FiixCmms.Client.Api.Crud;
using FiixCmms.Client.Models;

namespace FiixCmms.Client.Cli.Examples;

/// <summary>
/// Examples for Batch operations - executing multiple requests in a single transaction.
/// </summary>
public static class BatchExamples
{
    /// <summary>
    /// Example: Batch Find - Retrieve multiple objects in a single request.
    /// </summary>
    public static async Task BatchFindExampleAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Batch Find Example                                    ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        Console.WriteLine("📦 Preparing batch request with multiple find operations...");

        // Prepare the batch request
        var batch = client.PrepareBatch();

        // Add multiple find requests to the batch
        // Find request 1: Get first asset
        var findReq1 = client.PrepareFind<Asset>();
        findReq1.Fields = "id, strName, strInventoryCode";
        findReq1.Filters = new List<FindFilter>
        {
            new FindFilter { Ql = "intSiteID=?", Parameters = new List<object> { 1 } }
        };
        findReq1.Limit = 1;
        batch.Requests.Add(findReq1);

        // Find request 2: Get first work order
        var findReq2 = client.PrepareFind<WorkOrder>();
        findReq2.Fields = "id, strDescription, dtmDateSubmitted";
        findReq2.Limit = 1;
        batch.Requests.Add(findReq2);

        // Find request 3: Get first user
        var findReq3 = client.PrepareFind<User>();
        findReq3.Fields = "id, strFullName, strEmailAddress";
        findReq3.Limit = 1;
        batch.Requests.Add(findReq3);

        Console.WriteLine($"  • Request 1: Find first Asset");
        Console.WriteLine($"  • Request 2: Find first WorkOrder");
        Console.WriteLine($"  • Request 3: Find first User");
        Console.WriteLine($"\n🚀 Executing batch request...");

        var response = await client.BatchAsync(batch);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Batch Failed");
            Console.WriteLine($"Error: {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✅ Batch Request Successful!");
        Console.ResetColor();
        Console.WriteLine($"Received {response.Responses.Count} responses");

        for (int i = 0; i < response.Responses.Count; i++)
        {
            var resp = response.Responses[i];
            Console.WriteLine($"\n--- Response {i + 1} ---");
            if (resp.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Error: {resp.Error.Message}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Success! (Response Type: {resp.GetType().Name})");
            }
        }
    }

    /// <summary>
    /// Example: Batch Add - Create multiple objects in a single transactional request.
    /// </summary>
    public static async Task BatchAddExampleAsync(FiixCmmsSettings settings, int siteId)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Batch Add Example (Transactional)                     ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        Console.WriteLine("📦 Preparing batch request to create multiple assets...");
        Console.WriteLine("⚠️  Note: This is transactional - if any add fails, all are rolled back");

        // Prepare the batch request
        var batch = client.PrepareBatch();

        // Create three assets in a single batch
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        for (int i = 1; i <= 3; i++)
        {
            var addReq = client.PrepareAdd<Asset>();
            addReq.Fields = "id, strName, strInventoryCode";
            addReq.Object = new Asset
            {
                StrName = $"Batch Test Asset {i} - {timestamp}",
                StrInventoryCode = $"BATCH{i}-{timestamp}",
                IntSiteID = siteId
            };
            batch.Requests.Add(addReq);
            Console.WriteLine($"  • Asset {i}: {addReq.Object.StrName}");
        }

        Console.WriteLine($"\n🚀 Executing transactional batch request...");

        var response = await client.BatchAsync(batch);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Batch Failed - All operations rolled back");
            Console.WriteLine($"Error: {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✅ Batch Add Successful! All {response.Responses.Count} assets created.");
        Console.ResetColor();

        // Show created asset IDs
        for (int i = 0; i < response.Responses.Count; i++)
        {
            var resp = response.Responses[i];
            Console.WriteLine($"\n--- Asset {i + 1} ---");
            if (resp.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Error: {resp.Error.Message}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Created successfully (Response: {resp.GetType().Name})");
            }
        }
    }

    /// <summary>
    /// Example: Mixed Batch - Combine different request types (Add, Find, RPC, etc.)
    /// </summary>
    public static async Task MixedBatchExampleAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Mixed Batch Example                                   ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        Console.WriteLine("📦 Preparing mixed batch request (RPC + Find)...");

        // Prepare the batch request
        var batch = client.PrepareBatch();

        // Add an RPC request
        var pingReq = client.PrepareRpc();
        pingReq.Name = "Ping";
        batch.Requests.Add(pingReq);
        Console.WriteLine("  • RPC: Ping");

        // Add an RPC request for timezone
        var timezoneReq = client.PrepareRpc();
        timezoneReq.Name = "GetTimezone";
        batch.Requests.Add(timezoneReq);
        Console.WriteLine("  • RPC: GetTimezone");

        // Add a Find request
        var findReq = client.PrepareFind<Asset>();
        findReq.Fields = "id, strName";
        findReq.Limit = 1;
        batch.Requests.Add(findReq);
        Console.WriteLine("  • Find: First Asset");

        Console.WriteLine($"\n🚀 Executing mixed batch request...");

        var response = await client.BatchAsync(batch);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Batch Failed");
            Console.WriteLine($"Error: {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✅ Mixed Batch Successful!");
        Console.ResetColor();
        Console.WriteLine($"Received {response.Responses.Count} responses");

        string[] requestTypes = { "Ping", "GetTimezone", "FindRequest" };
        for (int i = 0; i < response.Responses.Count; i++)
        {
            var resp = response.Responses[i];
            Console.WriteLine($"\n--- {requestTypes[i]} Response ---");
            if (resp.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Error: {resp.Error.Message}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"✅ Success");
            }
        }
    }

    /// <summary>
    /// Run all batch examples.
    /// </summary>
    public static async Task RunAllAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Running All Batch Examples                            ║
╚════════════════════════════════════════════════════════════════╝

Batch requests allow you to:
  • Execute multiple operations in a single HTTP request
  • Reduce network overhead and latency
  • Ensure transactional integrity (all-or-nothing)
  • Combine different request types (CRUD, RPC, etc.)

");

        try
        {
            // 1. Batch Find
            await BatchFindExampleAsync(settings);
            Console.WriteLine("\n" + new string('─', 64) + "\n");

            // 2. Mixed Batch
            await MixedBatchExampleAsync(settings);

            Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         ✅ Batch Examples Complete                             ║
╚════════════════════════════════════════════════════════════════╝

Key Takeaways:
  ✓ Batch requests are transactional (all succeed or all fail)
  ✓ Responses are returned in the same order as requests
  ✓ Can combine any request types (CRUD, RPC, etc.)
  ✓ Reduces network overhead for multiple operations

For a batch add example (requires valid site ID):
  dotnet run -- batch add <site-id>
");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Batch Examples Failed: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }
}
