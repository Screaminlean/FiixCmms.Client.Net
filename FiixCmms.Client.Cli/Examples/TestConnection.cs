namespace FiixCmms.Client.Cli.Examples;

/// <summary>
/// Test connection and authentication.
/// </summary>
public static class TestConnection
{
    public static async Task RunAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Testing Connection & Authentication                   ║
╚════════════════════════════════════════════════════════════════╝
");

        Console.WriteLine("Configuration:");
        Console.WriteLine($"  Base URI: {settings.BaseUri}");
        Console.WriteLine($"  App Key: {MaskKey(settings.AppKey)}");
        Console.WriteLine($"  Access Key: {MaskKey(settings.AccessKey)}");
        Console.WriteLine($"  Secret Key: {MaskKey(settings.SecretKey)}");

        try
        {
            var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
            var client = new FiixCmmsClient(credentials, settings.BaseUri);

            Console.WriteLine("\n🔄 Attempting to find a single entity to test connection...");

            // Try to find any asset (limit 1)
            var request = client.PrepareFind<FiixCmms.Client.Models.Asset>();
            request.Fields = "id, strName";
            request.Limit = 1;

            var response = await client.FindAsync(request);

            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Connection Test Failed");
                Console.WriteLine($"Error: {response.Error.Message}");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Connection Successful!");
            Console.ResetColor();
            Console.WriteLine($"Found {response.TotalObjects} total assets in the system");
            if (response.Objects?.Count > 0)
            {
                Console.WriteLine($"Sample asset: ID={response.Objects[0].Id}, Name={response.Objects[0].StrName}");
            }

            Console.WriteLine("\n✅ Authentication is working correctly!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Connection Test Failed");
            Console.WriteLine($"Exception: {ex.Message}");
            Console.ResetColor();

            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }

    private static string MaskKey(string key)
    {
        if (string.IsNullOrEmpty(key) || key.Length < 8)
            return "****";

        return key.Substring(0, 4) + "****" + key.Substring(key.Length - 4);
    }
}
