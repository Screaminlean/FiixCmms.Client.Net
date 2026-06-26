using FiixCmms.Client.Models;

namespace FiixCmms.Client.Cli.Examples;

/// <summary>
/// Examples for finding/querying entities.
/// </summary>
public static class FindExamples
{
    public static async Task FindByIdAsync(FiixCmmsSettings settings, string entityType, long id)
    {
        Console.WriteLine($"\n🔍 Finding {entityType} by ID: {id}");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        switch (entityType.ToLower())
        {
            case "asset":
                await FindAssetByIdAsync(client, id);
                break;
            case "workorder":
                await FindWorkOrderByIdAsync(client, id);
                break;
            case "user":
                await FindUserByIdAsync(client, id);
                break;
            default:
                Console.WriteLine($"❌ Entity type '{entityType}' not supported in this example.");
                Console.WriteLine("Supported types: Asset, WorkOrder, User");
                break;
        }
    }

    public static async Task FindAllAsync(FiixCmmsSettings settings, string entityType)
    {
        Console.WriteLine($"\n🔍 Finding all {entityType} entities (first 10)");

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        switch (entityType.ToLower())
        {
            case "asset":
                await FindAllAssetsAsync(client);
                break;
            case "workorder":
                await FindAllWorkOrdersAsync(client);
                break;
            case "user":
                await FindAllUsersAsync(client);
                break;
            default:
                Console.WriteLine($"❌ Entity type '{entityType}' not supported in this example.");
                Console.WriteLine("Supported types: Asset, WorkOrder, User");
                break;
        }
    }

    private static async Task FindAssetByIdAsync(FiixCmmsClient client, long id)
    {
        var request = client.PrepareFindById<Asset>();
        request.Id = id;
        request.Fields = "id, strName, strCode, strDescription, intCategoryID, dv_intCategoryID";

        var response = await client.FindByIdAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return;
        }

        var asset = response.Object!;
        Console.WriteLine($"✅ Found Asset:");
        Console.WriteLine($"  ID: {asset.Id}");
        Console.WriteLine($"  Name: {asset.StrName}");
        Console.WriteLine($"  Code: {asset.StrCode}");
        Console.WriteLine($"  Description: {asset.StrDescription}");
        Console.WriteLine($"  Category ID: {asset.IntCategoryID}");

        if (asset.ExtraFields?.ContainsKey("dv_intCategoryID") == true)
        {
            Console.WriteLine($"  Category: {asset.ExtraFields["dv_intCategoryID"]}");
        }
    }

    private static async Task FindWorkOrderByIdAsync(FiixCmmsClient client, long id)
    {
        var request = client.PrepareFindById<WorkOrder>();
        request.Id = id;
        request.Fields = "id, strCode, strDescription, intPriorityID, intWorkOrderStatusID, dv_intPriorityID, dv_intWorkOrderStatusID";

        var response = await client.FindByIdAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return;
        }

        var wo = response.Object!;
        Console.WriteLine($"✅ Found WorkOrder:");
        Console.WriteLine($"  ID: {wo.Id}");
        Console.WriteLine($"  Code: {wo.StrCode}");
        Console.WriteLine($"  Description: {wo.StrDescription}");
        Console.WriteLine($"  Priority ID: {wo.IntPriorityID}");
        Console.WriteLine($"  Status ID: {wo.IntWorkOrderStatusID}");

        if (wo.ExtraFields?.ContainsKey("dv_intPriorityID") == true)
        {
            Console.WriteLine($"  Priority: {wo.ExtraFields["dv_intPriorityID"]}");
        }
        if (wo.ExtraFields?.ContainsKey("dv_intWorkOrderStatusID") == true)
        {
            Console.WriteLine($"  Status: {wo.ExtraFields["dv_intWorkOrderStatusID"]}");
        }
    }

    private static async Task FindUserByIdAsync(FiixCmmsClient client, long id)
    {
        var request = client.PrepareFindById<User>();
        request.Id = id;
        request.Fields = "id, strUserName, strFullName, strEmailAddress, intUserStatusID";

        var response = await client.FindByIdAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return;
        }

        var user = response.Object!;
        Console.WriteLine($"✅ Found User:");
        Console.WriteLine($"  ID: {user.Id}");
        Console.WriteLine($"  Username: {user.StrUserName}");
        Console.WriteLine($"  Full Name: {user.StrFullName}");
        Console.WriteLine($"  Email: {user.StrEmailAddress}");
        Console.WriteLine($"  Status ID: {user.IntUserStatusID}");
    }

    private static async Task FindAllAssetsAsync(FiixCmmsClient client)
    {
        var request = client.PrepareFind<Asset>();
        request.Fields = "id, strName, strCode";
        request.Limit = 10;
        request.OrderBy = "id";

        var response = await client.FindAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return;
        }

        Console.WriteLine($"✅ Found {response.TotalObjects} assets (showing first {response.Objects?.Count ?? 0}):");
        foreach (var asset in response.Objects ?? new List<Asset>())
        {
            Console.WriteLine($"  {asset.Id}: {asset.StrName} ({asset.StrCode})");
        }
    }

    private static async Task FindAllWorkOrdersAsync(FiixCmmsClient client)
    {
        var request = client.PrepareFind<WorkOrder>();
        request.Fields = "id, strCode, strDescription";
        request.Limit = 10;
        request.OrderBy = "id DESC";

        var response = await client.FindAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return;
        }

        Console.WriteLine($"✅ Found {response.TotalObjects} work orders (showing first {response.Objects?.Count ?? 0}):");
        foreach (var wo in response.Objects ?? new List<WorkOrder>())
        {
            Console.WriteLine($"  {wo.Id}: {wo.StrCode} - {wo.StrDescription}");
        }
    }

    private static async Task FindAllUsersAsync(FiixCmmsClient client)
    {
        var request = client.PrepareFind<User>();
        request.Fields = "id, strUserName, strFullName";
        request.Limit = 10;
        request.OrderBy = "strFullName";

        var response = await client.FindAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return;
        }

        Console.WriteLine($"✅ Found {response.TotalObjects} users (showing first {response.Objects?.Count ?? 0}):");
        foreach (var user in response.Objects ?? new List<User>())
        {
            Console.WriteLine($"  {user.Id}: {user.StrFullName} ({user.StrUserName})");
        }
    }
}
