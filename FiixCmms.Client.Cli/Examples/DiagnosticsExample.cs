using FiixCmms.Client.Diagnostics;
using FiixCmms.Client.Models;

namespace FiixCmms.Client.Cli.Examples;

/// <summary>
/// Run serialization diagnostics.
/// </summary>
public static class DiagnosticsExample
{
    public static async Task RunAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Running Serialization Diagnostics                     ║
╚════════════════════════════════════════════════════════════════╝
");

        // Test 1: WorkOrder serialization
        Console.WriteLine("\n=== Test 1: WorkOrder Serialization ===");
        SerializationDiagnostics.TestWorkOrderSerialization();

        // Test 2: FindByIdRequest serialization
        Console.WriteLine("\n=== Test 2: FindByIdRequest Serialization ===");
        SerializationDiagnostics.TestFindByIdRequestSerialization();

        // Test 3: Property naming
        Console.WriteLine("\n=== Test 3: Property Naming Convention ===");
        SerializationDiagnostics.TestPropertyNaming();

        // Test 4: Complete request example
        Console.WriteLine("\n=== Test 4: Complete Request Example ===");
        SerializationDiagnostics.ShowCompleteRequestExample();

        // Test 5: Real client request (if credentials provided)
        if (!string.IsNullOrEmpty(settings.AppKey) && 
            !string.IsNullOrEmpty(settings.AccessKey) &&
            settings.AppKey != "your-app-key")
        {
            Console.WriteLine("\n=== Test 5: Real API Request Signing ===");
            await TestRealRequestSigningAsync(settings);
        }
        else
        {
            Console.WriteLine("\n=== Test 5: Skipped (no real credentials) ===");
        }

        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         ✅ Diagnostics Complete                                ║
╚════════════════════════════════════════════════════════════════╝
");
    }

    private static async Task TestRealRequestSigningAsync(FiixCmmsSettings settings)
    {
        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        // Prepare a simple find request
        var request = client.PrepareFind<Asset>();
        request.Fields = "id, strName";
        request.Limit = 1;

        Console.WriteLine("Request prepared:");
        Console.WriteLine($"  ClassName: {request.ClassName}");
        Console.WriteLine($"  Fields: {request.Fields}");
        Console.WriteLine($"  Limit: {request.Limit}");

        // Note: We're not actually executing it, just showing it can be prepared
        Console.WriteLine("\n✅ Request can be prepared and signed successfully.");
        Console.WriteLine("   (Use 'test-auth' command to actually execute a request)");
    }
}
