using FiixCmms.Client.Cli;
using FiixCmms.Client.Cli.Examples;
using Microsoft.Extensions.Configuration;

// Build configuration from appsettings.json and environment variables
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var settings = config.GetSection("FiixCmms").Get<FiixCmmsSettings>();

if (settings == null || string.IsNullOrEmpty(settings.BaseUri))
{
    Console.WriteLine("Error: Configuration not found. Please configure appsettings.json or appsettings.local.json");
    return 1;
}

// Parse command-line arguments
var command = args.Length > 0 ? args[0].ToLower() : "help";

try
{
    switch (command)
    {
        case "crud":
            await CrudExample.RunCompleteExampleAsync(settings);
            break;

        case "find":
            var entityType = args.Length > 1 ? args[1] : "Asset";
            var id = args.Length > 2 ? long.Parse(args[2]) : 0;
            if (id > 0)
                await FindExamples.FindByIdAsync(settings, entityType, id);
            else
                await FindExamples.FindAllAsync(settings, entityType);
            break;

        case "add":
            await AddExample.RunAsync(settings, args.Skip(1).ToArray());
            break;

        case "change":
            await ChangeExample.RunAsync(settings, args.Skip(1).ToArray());
            break;

        case "remove":
            await RemoveExample.RunAsync(settings, args.Skip(1).ToArray());
            break;

        case "test-auth":
            await TestConnection.RunAsync(settings);
            break;

        case "diagnostics":
            await DiagnosticsExample.RunAsync(settings);
            break;

        case "rpc":
            var rpcCommand = args.Length > 1 ? args[1].ToLower() : "all";
            switch (rpcCommand)
            {
                case "ping":
                    await RpcExamples.PingServerAsync(settings);
                    break;
                case "timezone":
                    await RpcExamples.GetTimezoneAsync(settings);
                    break;
                case "sites":
                    await RpcExamples.GetAccessibleSitesAsync(settings);
                    break;
                case "custom-fields":
                    await RpcExamples.CustomFieldsExampleAsync(settings);
                    break;
                case "comprehensive":
                case "comp":
                    await FiixCmms.Client.Cli.Examples.Rpc.ComprehensiveRpcExamples.RunAllAsync(settings);
                    break;
                case "all":
                default:
                    await RpcExamples.RunAllAsync(settings);
                    break;
            }
            break;

        case "batch":
            var batchCommand = args.Length > 1 ? args[1].ToLower() : "all";
            switch (batchCommand)
            {
                case "find":
                    await BatchExamples.BatchFindExampleAsync(settings);
                    break;
                case "add":
                    if (args.Length > 2 && int.TryParse(args[2], out var siteId))
                        await BatchExamples.BatchAddExampleAsync(settings, siteId);
                    else
                    {
                        Console.WriteLine("Usage: batch add <site-id>");
                        Console.WriteLine("Example: batch add 1");
                    }
                    break;
                case "mixed":
                    await BatchExamples.MixedBatchExampleAsync(settings);
                    break;
                case "all":
                default:
                    await BatchExamples.RunAllAsync(settings);
                    break;
            }
            break;

        case "help":
        case "--help":
        case "-h":
        default:
            ShowHelp();
            break;
    }

    return 0;
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n? Error: {ex.Message}");
    Console.ResetColor();
    if (ex.InnerException != null)
    {
        Console.WriteLine($"   Inner: {ex.InnerException.Message}");
    }
    return 1;
}

static void ShowHelp()
{
    Console.WriteLine(@"
+----------------------------------------------------------------+
¦         Fiix CMMS .NET Client - Testing Tool                  ¦
+----------------------------------------------------------------+

USAGE:
  fiix-cmms-client-cmd [command] [options]

COMMANDS:
  crud                  - Run complete CRUD example (matching Java examples)
  find <type> [id]      - Find entities by type (e.g., Asset, WorkOrder)
                          If ID provided, finds by ID; otherwise lists all
  add                   - Add new entity (interactive)
  change                - Change existing entity (interactive)
  remove <type> <id>    - Remove entity by ID
  rpc [command]         - Run RPC (Remote Procedure Call) examples
                          ping: Test server connectivity
                          timezone: Get server timezone
                          sites: Get accessible sites
                          custom-fields: Custom fields RPC example
                          comprehensive: Test all documented RPC methods
                          all: Run basic RPC examples (default)
  batch [command]       - Run Batch (transactional multi-request) examples
                          find: Batch find multiple entities
                          mixed: Mixed batch (RPC + CRUD)
                          add <site-id>: Batch add multiple assets
                          all: Run all batch examples (default)
  test-auth             - Test authentication and connection
  diagnostics           - Run serialization diagnostics
  help                  - Show this help message

EXAMPLES:
  # Run complete CRUD example (create, read, update, delete)
  fiix-cmms-client-cmd crud

  # Test connection
  fiix-cmms-client-cmd test-auth

  # Ping the server (RPC call)
  fiix-cmms-client-cmd rpc ping

  # Get server timezone
  fiix-cmms-client-cmd rpc timezone

  # Get accessible sites
  fiix-cmms-client-cmd rpc sites

  # Run basic RPC examples
  fiix-cmms-client-cmd rpc

  # Test all documented RPC methods
  fiix-cmms-client-cmd rpc comprehensive

  # Run batch examples
  fiix-cmms-client-cmd batch

  # Batch find multiple entities
  fiix-cmms-client-cmd batch find

  # Batch add multiple assets (requires site ID)
  fiix-cmms-client-cmd batch add 1

  # Mixed batch (RPC + CRUD)
  fiix-cmms-client-cmd batch mixed

  # Find all assets
  fiix-cmms-client-cmd find Asset

  # Find asset by ID
  fiix-cmms-client-cmd find Asset 12345

  # Find work order by ID
  fiix-cmms-client-cmd find WorkOrder 67890

  # Remove an asset
  fiix-cmms-client-cmd remove Asset 12345

  # Run diagnostics
  fiix-cmms-client-cmd diagnostics

CONFIGURATION:
  Configure credentials in appsettings.json or appsettings.local.json:
  {
    ""FiixCmms"": {
      ""BaseUri"": ""https://yourinstance.fiixsandbox.com/api/"",
      ""AppKey"": ""your-app-key"",
      ""AccessKey"": ""your-access-key"",
      ""SecretKey"": ""your-secret-key""
    }
  }

  Or use environment variables:
  - FiixCmms__BaseUri
  - FiixCmms__AppKey
  - FiixCmms__AccessKey
  - FiixCmms__SecretKey

DOCUMENTATION:
  See docs/API_CONTRACT_ANALYSIS.md for API details
  See VERIFICATION_COMPLETE.md for verification status

");
}

