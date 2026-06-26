namespace FiixCmms.Client.Cli.Examples.Rpc;

/// <summary>
/// Comprehensive examples for Fiix CMMS RPC operations.
/// </summary>
public static class ComprehensiveRpcExamples
{
    /// <summary>
    /// Run all RPC examples with detailed output.
    /// </summary>
    public static async Task RunAllAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Comprehensive RPC Examples                            ║
║         Testing All Available RPC Methods                     ║
╚════════════════════════════════════════════════════════════════╝
");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        try
        {
            // 1. Basic Connectivity
            await PingExample(client);
            await GetTimezoneExample(client);
            await GetAccessibleSitesExample(client);

            Console.WriteLine("\n" + new string('═', 64));

            // 2. Asset Operations
            await AssetResolvedExample(client);

            Console.WriteLine("\n" + new string('═', 64));

            // 3. Custom Fields
            await CustomFieldsExample(client);

            Console.WriteLine("\n" + new string('═', 64));

            // 4. Work Order Operations
            await WorkOrderLogExample(client);

            Console.WriteLine("\n" + new string('═', 64));

            // 5. Audit & Activity
            await ActivityLogExample(client);
            await AuditTrailExample(client);

            Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         ✅ All RPC Examples Complete                           ║
╚════════════════════════════════════════════════════════════════╝
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

    #region Basic Connectivity Examples

    public static async Task PingExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n📡 Testing: Ping");
        Console.WriteLine("Purpose: Verify server connectivity");

        var response = await AllRpcMethods.PingAsync(client);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✅ Success: Server is up and running!");
        Console.ResetColor();

        if (response.Data != null)
        {
            Console.WriteLine($"Response: {response.Data}");
        }
    }

    public static async Task GetTimezoneExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n🌍 Testing: GetTimezone");
        Console.WriteLine("Purpose: Get server timezone information");

        var response = await AllRpcMethods.GetTimezoneAsync(client);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✅ Success: Timezone retrieved");
        Console.ResetColor();

        if (response.Data != null)
        {
            Console.WriteLine($"Timezone Data: {response.Data}");
        }
    }

    public static async Task GetAccessibleSitesExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n🏢 Testing: GetAccessibleSites");
        Console.WriteLine("Purpose: Get all sites accessible to current user");

        var response = await AllRpcMethods.GetAccessibleSitesAsync(client);

        if (response.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  {response.Error.Message}");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✅ Success: Accessible sites retrieved");
        Console.ResetColor();

        if (response.Data != null)
        {
            Console.WriteLine($"Sites: {response.Data}");
        }
    }

    #endregion

    #region Asset Examples

    public static async Task AssetResolvedExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n🏭 Testing: AssetResolved");
        Console.WriteLine("Purpose: Get resolved asset information with related data");

        try
        {
            var parameters = new Dictionary<string, object>
            {
                { "limit", 5 },
                { "page", 0 }
            };

            var response = await AllRpcMethods.GetAssetResolvedAsync(client, parameters);

            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️  {response.Error.Message}");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Success: Asset data retrieved");
            Console.ResetColor();

            if (response.Data != null)
            {
                Console.WriteLine($"Response Data: {response.Data}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  Note: {ex.Message}");
            Console.WriteLine("This RPC may require specific parameters for your instance.");
            Console.ResetColor();
        }
    }

    #endregion

    #region Custom Fields Examples

    public static async Task CustomFieldsExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n📋 Testing: CustomFields RPC");
        Console.WriteLine("Purpose: Access custom field metadata");

        try
        {
            // Example 1: Get Work Order Custom Fields Metadata
            Console.WriteLine("\n  → Action: getWorkOrderCustomFieldsMetaData");

            var response = await AllRpcMethods.CustomFieldsAsync<dynamic>(
                client,
                action: "getWorkOrderCustomFieldsMetaData"
            );

            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  ⚠️  {response.Error.Message}");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ✅ Success: Custom fields metadata retrieved");
            Console.ResetColor();

            if (response.Data != null)
            {
                Console.WriteLine($"  Data: {response.Data}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  Note: {ex.Message}");
            Console.WriteLine("Custom fields RPC requires proper setup in your Fiix instance.");
            Console.ResetColor();
        }
    }

    #endregion

    #region Work Order Examples

    public static async Task WorkOrderLogExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n📝 Testing: WorkOrderLog");
        Console.WriteLine("Purpose: Get work order log entries");

        try
        {
            var parameters = new Dictionary<string, object>
            {
                { "workOrderId", 1 }, // Replace with actual work order ID
                { "limit", 10 }
            };

            var response = await AllRpcMethods.GetWorkOrderLogAsync(client, parameters);

            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️  {response.Error.Message}");
                Console.ResetColor();
                Console.WriteLine("Note: This requires a valid work order ID from your instance.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Success: Work order log retrieved");
            Console.ResetColor();

            if (response.Data != null)
            {
                Console.WriteLine($"Log Data: {response.Data}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  Note: {ex.Message}");
            Console.WriteLine("This example requires a valid work order ID.");
            Console.ResetColor();
        }
    }

    #endregion

    #region Audit & Activity Examples

    public static async Task ActivityLogExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n📊 Testing: ActivityLog");
        Console.WriteLine("Purpose: Get activity log entries");

        try
        {
            var parameters = new Dictionary<string, object>
            {
                { "limit", 10 },
                { "offset", 0 }
            };

            var response = await AllRpcMethods.GetActivityLogAsync(client, parameters);

            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️  {response.Error.Message}");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Success: Activity log retrieved");
            Console.ResetColor();

            if (response.Data != null)
            {
                Console.WriteLine($"Activity Data: {response.Data}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  Note: {ex.Message}");
            Console.ResetColor();
        }
    }

    public static async Task AuditTrailExample(FiixCmmsClient client)
    {
        Console.WriteLine("\n🔍 Testing: AuditTrail");
        Console.WriteLine("Purpose: Get audit trail information");

        try
        {
            var parameters = new Dictionary<string, object>
            {
                { "entityType", "Asset" },
                { "limit", 5 }
            };

            var response = await AllRpcMethods.GetAuditTrailAsync(client, parameters);

            if (response.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️  {response.Error.Message}");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Success: Audit trail retrieved");
            Console.ResetColor();

            if (response.Data != null)
            {
                Console.WriteLine($"Audit Data: {response.Data}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠️  Note: {ex.Message}");
            Console.ResetColor();
        }
    }

    #endregion
}
