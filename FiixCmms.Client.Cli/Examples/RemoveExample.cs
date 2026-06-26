using FiixCmms.Client.Models;

namespace FiixCmms.Client.Cli.Examples;

/// <summary>
/// Remove (delete) example.
/// </summary>
public static class RemoveExample
{
    public static async Task RunAsync(FiixCmmsSettings settings, string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("❌ Usage: remove <EntityType> <ID>");
            Console.WriteLine("Example: remove Asset 12345");
            return;
        }

        var entityType = args[0];
        if (!long.TryParse(args[1], out var id))
        {
            Console.WriteLine($"❌ Invalid ID: {args[1]}");
            return;
        }

        Console.WriteLine($"\n🗑️  Removing {entityType} with ID: {id}");
        Console.WriteLine("⚠️  WARNING: This will permanently delete the entity!");
        Console.Write("Are you sure? (yes/no): ");

        var confirmation = Console.ReadLine()?.Trim().ToLower();
        if (confirmation != "yes")
        {
            Console.WriteLine("❌ Cancelled.");
            return;
        }

        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        var client = new FiixCmmsClient(credentials, settings.BaseUri);

        switch (entityType.ToLower())
        {
            case "asset":
                await RemoveEntityAsync<Asset>(client, id);
                break;
            case "workorder":
                await RemoveEntityAsync<WorkOrder>(client, id);
                break;
            case "user":
                await RemoveEntityAsync<User>(client, id);
                break;
            default:
                Console.WriteLine($"❌ Entity type '{entityType}' not supported.");
                Console.WriteLine("Supported types: Asset, WorkOrder, User");
                break;
        }
    }

    private static async Task RemoveEntityAsync<T>(FiixCmmsClient client, long id) where T : FiixCmms.Client.Models.ClientCmmsDto
    {
        var request = client.PrepareRemove<T>();
        request.Id = id;

        var response = await client.RemoveAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return;
        }

        Console.WriteLine($"✅ Successfully removed {response.Count} entity with ID: {id}");
    }
}
