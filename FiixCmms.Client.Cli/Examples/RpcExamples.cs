using FiixCmms.Client.Cli.Examples.Rpc;

namespace FiixCmms.Client.Cli.Examples;

/// <summary>
/// Examples for RPC (Remote Procedure Call) operations.
/// </summary>
public static class RpcExamples
{
    /// <summary>
    /// Example: Ping the server to test connectivity.
    /// </summary>
    public static async Task PingServerAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Testing Server Connectivity (Ping RPC)                ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        Console.WriteLine("📡 Sending Ping request to server...");

        var response = await AllRpcMethods.PingAsync(client);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Ping Failed");
            Console.WriteLine($"Error: {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ Ping Successful!");
        Console.ResetColor();
        Console.WriteLine("You have successfully made an RPC call to the API.");
        Console.WriteLine("Server is up and running.");

        if (response.Data != null)
        {
            Console.WriteLine($"\nResponse Data: {response.Data}");
        }
    }

    /// <summary>
    /// Example: Get accessible sites.
    /// </summary>
    public static async Task GetAccessibleSitesAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Get Accessible Sites RPC                              ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        Console.WriteLine("🏢 Getting accessible sites...");

        var response = await AllRpcMethods.GetAccessibleSitesAsync(client);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Failed to get sites");
            Console.WriteLine($"Error: {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ Successfully retrieved accessible sites!");
        Console.ResetColor();

        if (response.Data != null)
        {
            Console.WriteLine($"\nSites Data: {response.Data}");
        }
    }

    /// <summary>
    /// Example: Get timezone information.
    /// </summary>
    public static async Task GetTimezoneAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Get Timezone RPC                                      ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        Console.WriteLine("🌍 Getting server timezone...");

        var response = await AllRpcMethods.GetTimezoneAsync(client);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Failed to get timezone");
            Console.WriteLine($"Error: {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ Successfully retrieved timezone!");
        Console.ResetColor();

        if (response.Data != null)
        {
            Console.WriteLine($"\nTimezone: {response.Data}");
        }
    }

    /// <summary>
    /// Example: Call a parameterized RPC method.
    /// This is a placeholder showing the pattern - actual CustomFields RPC may require specific setup.
    /// </summary>
    public static async Task CustomFieldsExampleAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Custom Fields RPC Example                             ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        Console.WriteLine("📋 Calling CustomFields.getWorkOrderCustomFieldsMetaData RPC...");

        try
        {
            var response = await AllRpcMethods.CustomFieldsAsync<dynamic>(
                client,
                action: "getWorkOrderCustomFieldsMetaData"
            );

            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n⚠️ RPC Call Response:");
                Console.WriteLine($"Error: {response.Error.Message}");
                Console.ResetColor();
                Console.WriteLine("\nNote: This is an example pattern. You may need to:");
                Console.WriteLine("  1. Create a specific response DTO class");
                Console.WriteLine("  2. Configure custom fields in your Fiix instance");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Custom Fields RPC Successful!");
            Console.ResetColor();

            if (response.Data != null)
            {
                Console.WriteLine($"\nResponse Data: {response.Data}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n⚠️ Exception: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("\nThis is expected if custom fields haven't been set up.");
            Console.WriteLine("The RPC infrastructure is working correctly.");
        }
    }

    /// <summary>
    /// Run all RPC examples.
    /// </summary>
    public static async Task RunAllAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Running All RPC Examples                              ║
╚════════════════════════════════════════════════════════════════╝
");

        try
        {
            // 1. Ping - Most basic RPC
            await PingServerAsync(settings);

            Console.WriteLine("\n" + new string('─', 64) + "\n");

            // 2. GetTimezone - System info RPC
            await GetTimezoneAsync(settings);

            Console.WriteLine("\n" + new string('─', 64) + "\n");

            // 3. GetAccessibleSites - Data retrieval RPC
            await GetAccessibleSitesAsync(settings);

            Console.WriteLine("\n" + new string('─', 64) + "\n");

            // 4. Custom Fields - Parameterized RPC example
            await CustomFieldsExampleAsync(settings);

            Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         ✅ RPC Examples Complete                               ║
╚════════════════════════════════════════════════════════════════╝

Available RPC Methods:
  • Ping                         - Test connectivity
  • GetTimezone                  - Get server timezone
  • GetAccessibleSites           - Get available sites
  • AssetResolved                - Get resolved asset data
  • Calendar                     - Calendar operations
  • CustomFields                 - Custom field operations
  • ActivityLog                  - Activity logging
  • AuditTrail                   - Audit information
  • WorkOrderLog                 - Work order logs
  • DashboardWidget              - Dashboard data
  • DataExport                   - Export operations
  • FollowOnWorkOrders          - Follow-on work orders
  • StocksReceived              - Inventory operations
  • ScheduleTrigger*            - Scheduling operations
  • TaskGroupsToWorkOrder       - Task conversion

For comprehensive examples of all RPC methods:
  dotnet run -- rpc comprehensive
");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ RPC Examples Failed: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }
}
