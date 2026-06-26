using FiixCmms.Client.Api.Crud;
using FiixCmms.Client.Models;

namespace FiixCmms.Client.Cli.Examples;

/// <summary>
/// .NET port of the Java CrudRequestExample.java
/// Demonstrates CRUD operations matching the official Fiix Java examples.
/// Based on: https://github.com/fiixlabs/fiix-cmms-api-java-examples
/// </summary>
public class CrudExample
{
    private readonly FiixCmmsClient _client;
    private const long False = 0L;
    private const long True = 1L;
    private const int LocationSysCode = 1;
    private const string CA = "CA";

    public CrudExample(FiixCmmsSettings settings)
    {
        var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
        _client = new FiixCmmsClient(credentials, settings.BaseUri);
    }

    /// <summary>
    /// Example: Create an asset categorized as 'Locations and Facilities'.
    /// Matches Java: CrudRequestExample.addAnAssetWithCategoryLocation()
    /// </summary>
    public async Task<Asset> AddAnAssetWithCategoryLocationAsync()
    {
        Console.WriteLine("\n=== Adding Asset with Category Location ===");

        // Find the asset category id for locations
        var category = await FindAssetCategoryBySysCodeAsync(LocationSysCode);

        // Build an asset with field details
        var asset = new Asset
        {
            IntCategoryID = category.Id,
            StrName = "Toronto",
            StrCode = "WS-32",
            BolIsOnline = False, // set offline
            StrDescription = "Hi, I am an asset with category 'Locations And Facilities', I was created by a web service"
        };

        // Build an add request
        var request = _client.PrepareAdd<Asset>();
        request.Fields = "id, intCategoryID, strName, strCode, bolIsOnline, strDescription";
        request.Object = asset;

        // Finally, create
        var response = await _client.AddAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            throw new Exception($"Failed to add asset: {response.Error.Message}");
        }

        var responseAsset = response.Object!;
        Console.WriteLine($"✅ Created asset with ID: {responseAsset.Id}");
        Console.WriteLine($"  Name: {responseAsset.StrName}");
        Console.WriteLine($"  Code: {responseAsset.StrCode}");
        Console.WriteLine($"  Category ID: {responseAsset.IntCategoryID}");

        return responseAsset;
    }

    /// <summary>
    /// Example: Change (update) an asset.
    /// Matches Java: CrudRequestExample.change(Long assetId)
    /// </summary>
    public async Task<Asset> ChangeAsync(long assetId)
    {
        Console.WriteLine($"\n=== Updating Asset {assetId} ===");

        // Find a country, to obtain its id
        var country = await FindCountryByShortNameAsync(CA);

        // Create a change request and prepare an asset for change
        var request = _client.PrepareChange<Asset>();
        request.Object = new Asset
        {
            Id = assetId,
            StrName = "Greater Toronto Area",
            BolIsOnline = True,
            StrDescription = "Hi, I am an asset with category 'Locations And Facilities', I was updated by a web service",
            IntCountryID = country.Id
        };

        // List columns you would like to get back in the response object
        request.Fields = "id, intCategoryID, strName, strCode, bolIsOnline, strDescription, intCountryID";

        // List columns you would like to change
        request.ChangeFields = "strName, bolIsOnline, strDescription, intCountryID";

        var response = await _client.ChangeAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            throw new Exception($"Failed to change asset: {response.Error.Message}");
        }

        var responseAsset = response.Object!;
        Console.WriteLine($"✅ Updated asset ID: {responseAsset.Id}");
        Console.WriteLine($"  Name: {responseAsset.StrName}");
        Console.WriteLine($"  IsOnline: {responseAsset.BolIsOnline}");
        Console.WriteLine($"  Country ID: {responseAsset.IntCountryID}");

        return responseAsset;
    }

    /// <summary>
    /// Example: Find all assets with category 'Locations and Facilities'.
    /// Matches Java: CrudRequestExample.findAllLocations()
    /// </summary>
    public async Task<List<Asset>> FindAllLocationsAsync()
    {
        Console.WriteLine("\n=== Finding All Locations ===");

        var category = await FindAssetCategoryBySysCodeAsync(LocationSysCode);

        // Create filter using query language (Ql) with parameters
        var filter = new FindFilter
        {
            Ql = "intCategoryID=? AND bolIsSite=?",
            Parameters = new List<object> { category.Id!, False }
        };

        var request = _client.PrepareFind<Asset>();
        request.Fields = "id, strName, strCode, bolIsOnline, bolIsSite, intCategoryID";
        request.Filters = new List<FindFilter> { filter };
        request.OrderBy = "strName";

        var response = await _client.FindAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            return new List<Asset>();
        }

        Console.WriteLine($"✅ Total {response.TotalObjects} assets found with category '{category.StrName}'");
        foreach (var asset in response.Objects ?? new List<Asset>())
        {
            Console.WriteLine($"  {asset.Id}: {asset.StrName} ({asset.StrCode})");
        }

        return response.Objects ?? new List<Asset>();
    }

    /// <summary>
    /// Example: Find Asset Category by SysCode.
    /// Matches Java: CrudRequestExample.findAssetCategoryBySysCode(Integer sysCode)
    /// </summary>
    public async Task<AssetCategory> FindAssetCategoryBySysCodeAsync(int sysCode)
    {
        Console.WriteLine($"\n🔍 Finding Asset category by sysCode = {sysCode} ...");

        var filter = new FindFilter
        {
            Ql = "intSysCode=?",
            Parameters = new List<object> { sysCode }
        };

        var request = _client.PrepareFind<AssetCategory>();
        request.Fields = "id, strName, intSysCode";
        request.Filters = new List<FindFilter> { filter };

        var response = await _client.FindAsync(request);

        if (response.Error != null || response.Objects == null || response.Objects.Count == 0)
        {
            throw new Exception($"Failed to find asset category with sysCode {sysCode}");
        }

        var category = response.Objects[0];
        Console.WriteLine($"  ✅ Found: {category.StrName} (ID: {category.Id})");

        return category;
    }

    /// <summary>
    /// Example: Find an asset by ID with display value field.
    /// Matches Java: CrudRequestExample.findById(Long locationId)
    /// </summary>
    public async Task<Asset> FindByIdAsync(long locationId)
    {
        Console.WriteLine($"\n🔍 Finding location by id: {locationId}");

        var request = _client.PrepareFindById<Asset>();
        request.Fields = "id, strName, strCode, strDescription, dv_intCountryID";
        request.Id = locationId;

        var response = await _client.FindByIdAsync(request);

        if (response.Error != null || response.Object == null)
        {
            throw new Exception($"Failed to find asset with ID {locationId}");
        }

        var asset = response.Object;
        Console.WriteLine($"✅ Found asset:");
        Console.WriteLine($"  ID: {asset.Id}");
        Console.WriteLine($"  Code: {asset.StrCode}");
        Console.WriteLine($"  Name: {asset.StrName}");
        Console.WriteLine($"  Description: {asset.StrDescription}");

        // Display value fields are in ExtraFields
        if (asset.ExtraFields?.ContainsKey("dv_intCountryID") == true)
        {
            Console.WriteLine($"  Country: {asset.ExtraFields["dv_intCountryID"]}");
        }

        return asset;
    }

    /// <summary>
    /// Example: Find a Country by short name (2-letter code).
    /// Matches Java: CrudRequestExample.findCountryByShortName(String shortName)
    /// </summary>
    public async Task<Country> FindCountryByShortNameAsync(string shortName)
    {
        Console.WriteLine($"\n🔍 Finding Country by short code = {shortName} ...");

        var filter = new FindFilter
        {
            Ql = "strShort2=?",
            Parameters = new List<object> { shortName }
        };

        var request = _client.PrepareFind<Country>();
        request.Fields = "id, strName, strShort2";
        request.Filters = new List<FindFilter> { filter };

        var response = await _client.FindAsync(request);

        if (response.Error != null || response.Objects == null || response.Objects.Count == 0)
        {
            throw new Exception($"Failed to find country with short code {shortName}");
        }

        var country = response.Objects[0];
        Console.WriteLine($"  ✅ Found: {country.StrName} (ID: {country.Id})");

        return country;
    }

    /// <summary>
    /// Example: Remove (delete) an asset by ID.
    /// Matches Java: CrudRequestExample.remove(Long assetId)
    /// </summary>
    public async Task RemoveAsync(long assetId)
    {
        Console.WriteLine($"\n🗑️  Removing asset {assetId} ...");

        var request = _client.PrepareRemove<Asset>();
        request.Id = assetId;

        var response = await _client.RemoveAsync(request);

        if (response.Error != null)
        {
            Console.WriteLine($"❌ Error: {response.Error.Message}");
            throw new Exception($"Failed to remove asset: {response.Error.Message}");
        }

        Console.WriteLine($"✅ Removed {response.Count} asset, with id: {assetId}");
    }

    /// <summary>
    /// Run the complete CRUD cycle - exactly like the Java example.
    /// </summary>
    public static async Task RunCompleteExampleAsync(FiixCmmsSettings settings)
    {
        Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         Running Complete CRUD Example                         ║
║         (Matching Java CrudRequestExample)                    ║
╚════════════════════════════════════════════════════════════════╝
");

        var example = new CrudExample(settings);

        try
        {
            // Create
            var asset = await example.AddAnAssetWithCategoryLocationAsync();
            long assetId = asset.Id!.Value;

            // Update
            await example.ChangeAsync(assetId);

            // Read by Id
            await example.FindByIdAsync(assetId);

            // Read All
            await example.FindAllLocationsAsync();

            // Remove
            await example.RemoveAsync(assetId);

            Console.WriteLine(@"
╔════════════════════════════════════════════════════════════════╗
║         ✅ CRUD Example Completed Successfully!                ║
╚════════════════════════════════════════════════════════════════╝
");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ CRUD Example Failed: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }
}
