# Fiix CMMS .NET Client

A .NET 10 client library for interacting with the [Fiix CMMS API](https://fiixlabs.github.io/api-documentation/). Provides a strongly-typed, async interface for CRUD, RPC, and Batch operations.

**Verified against official Fiix CMMS API documentation (version 2.48.1).**

## Features

- ✅ **107 DTOs** — Complete model coverage for Fiix CMMS entities
- ✅ **CRUD operations** — `FindById`, `Find`, `Add`, `Change`, `Remove`
- ✅ **RPC operations** — All 18 documented RPC methods
- ✅ **Batch operations** — Atomic multi-operation execution in a single request
- ✅ **Async/await** — All I/O operations are async; no blocking wrappers
- ✅ **HMAC SHA256 authentication** — Request signing built in
- ✅ **Throttle handling** — Automatic retry on 429 responses
- ✅ **Nullable reference types** — Full .NET nullable support
- ✅ **.NET 10** — Targets the latest .NET version

## Design Philosophy

### Async-Only API

All network methods are `async` — there are no synchronous wrappers. This matches `HttpClient`, the Azure SDK, and EF Core, and avoids deadlocks in UI and ASP.NET contexts.

```csharp
// ✅ Correct
public async Task<List<Asset>> GetAssetsAsync()
{
    var request = _client.PrepareFind<Asset>();
    var response = await _client.FindAsync(request);
    return response.Objects ?? [];
}

// ❌ Avoid — .Result can deadlock in UI / ASP.NET contexts
var response = _client.FindAsync(request).Result;
```

For legacy synchronous contexts only:

```csharp
var response = client.FindAsync(request).GetAwaiter().GetResult();
```

See: [Don't Block on Async Code](https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html)

## Installation

Add a project reference to `FiixCmms.Client`:

```bash
dotnet add reference path/to/FiixCmms.Client/FiixCmms.Client.csproj
```

## Quick Start

```csharp
using FiixCmms.Client;
using FiixCmms.Client.Models;

var credentials = new BasicCredentials(
	appKey:    "your-app-key",
	accessKey: "your-access-key",
	secretKey: "your-secret-key"
);

var client = new FiixCmmsClient(
	credentials,
	baseUri: "https://yourinstance.fiixsandbox.com/api/"
);

// Find a work order by ID
var request = client.PrepareFindById<WorkOrder>();
request.Id = 12345;
request.Fields = "id, strCode, strDescription";

var response = await client.FindByIdAsync(request);

if (response.Error == null)
{
	Console.WriteLine($"Work Order: {response.Object?.StrCode}");
	Console.WriteLine($"Description: {response.Object?.StrDescription}");
}
else
{
	Console.WriteLine($"Error [{response.Error.Code}]: {response.Error.Message}");
}
```

## Usage Examples

### Find Multiple Records

```csharp
using FiixCmms.Client.Api.Crud;

// Find open work orders
var findRequest = client.PrepareFind<WorkOrder>();
findRequest.Fields  = "id, strCode, strDescription, intWorkOrderStatusID";
findRequest.Filters = new List<FindFilter>
{
	new FindFilter
	{
		Ql         = "intWorkOrderStatusID = ?",
		Parameters = new List<object> { 1 }
	}
};
findRequest.OrderBy = "strCode";
findRequest.Limit   = 10;

var response = await client.FindAsync(findRequest);

if (response.Error == null)
{
	foreach (var workOrder in response.Objects ?? [])
	{
		Console.WriteLine($"{workOrder.Id}: {workOrder.StrCode}");
	}
}
```

### Create a New Record

```csharp
// Add a new asset
var addRequest = client.PrepareAdd<Asset>();
addRequest.Object = new Asset
{
	StrName   = "New Equipment",
	StrCode   = "EQ-001",
	IntSiteID = 1
};
addRequest.Fields = "id, strName, strCode";

var response = await client.AddAsync(addRequest);

if (response.Error == null)
{
	Console.WriteLine($"Created asset with ID: {response.Object?.Id}");
}
```

### Update Records

```csharp
// Update a work order
var changeRequest = client.PrepareChange<WorkOrder>();
changeRequest.Object = new WorkOrder
{
	Id             = 12345,
	StrDescription = "Updated description",
	IntPriorityID  = 2
};
changeRequest.ChangeFields = "strDescription, intPriorityID"; // only these fields are written
changeRequest.Fields       = "id, strCode, strDescription";   // fields returned in response

var response = await client.ChangeAsync(changeRequest);
// response.Object -> WorkOrder? with updated values
```

### Delete Records

```csharp
// Remove a record by ID
var removeRequest = client.PrepareRemove<Asset>();
removeRequest.Id = 12345;

var response = await client.RemoveAsync(removeRequest);

if (response.Error == null)
{
	Console.WriteLine($"Removed {response.Count} record(s)");
}
```

### RPC (Remote Procedure Call) Operations

```csharp
// Ping the server
var rpcRequest = client.PrepareRpc();
rpcRequest.Name = "Ping";

var rpcResponse = await client.RpcAsync(rpcRequest);

if (rpcResponse.Error == null)
{
	Console.WriteLine("✅ Server is up and running!");
}
```

```csharp
// Parameterized RPC example
var request = client.PrepareParameterizedRpc();
request.Name = "CustomFields";
request.Action = "getCustomTableData";
request.Parameters = new Dictionary<string, object>
{
	{ "customTableName", "YourTableName" }
};

var response = await client.RpcAsync<CustomTableMetaData>(request);

if (response.Error == null && response.Data != null)
{
	Console.WriteLine($"Custom table: {response.Data.TableName}");
}
```

### Batch Operations

Execute multiple CRUD and/or RPC operations in a single HTTP request. The entire batch is transactional — if any operation fails, all are rolled back.

```csharp
var batch = client.PrepareBatch();

// Queue a find
var findReq = client.PrepareFind<Asset>();
findReq.Fields = "id, strName";
findReq.Limit  = 10;
batch.Requests.Add(findReq);

// Queue an RPC call
var pingReq = client.PrepareRpc();
pingReq.Name = "Ping";
batch.Requests.Add(pingReq);

var response = await client.BatchAsync(batch);
// response.Responses[0] -> find results
// response.Responses[1] -> ping result
```

> For best results keep batches to 10–50 operations. Responses are ordered to match requests.

> **Note:** For runnable examples see the CLI tool in `FiixCmms.Client.Cli/Examples/`.

## Available Models

The library includes 107 DTO models covering all major Fiix CMMS entities:

- **Assets**: `Asset`, `AssetCategory`, `AssetClassification`, etc.
- **Work Orders**: `WorkOrder`, `WorkOrderStatus`, `WorkOrderTask`, etc.
- **Maintenance**: `ScheduledMaintenance`, `MaintenanceType`, `MeterReading`, etc.
- **Purchasing**: `PurchaseOrder`, `Receipt`, `Stock`, etc.
- **Users**: `User`, `Business`, `RegionUser`, etc.
- **And many more...**

## Configuration

### Testing JSON Serialization

Before using with the real API, verify JSON output matches expectations:

```csharp
using FiixCmms.Client.Diagnostics;

SerializationDiagnostics.TestWorkOrderSerialization();
SerializationDiagnostics.TestFindByIdRequestSerialization();
SerializationDiagnostics.TestPropertyNaming();
// Outputs the actual JSON payload so you can verify:
//   strCode: "WO-001"  ✓  (not StrCode)
//   intSiteID: 1       ✓  (not intSiteId)
```

### Custom HTTP Client

```csharp
var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
var client = new FiixCmmsClient(credentials, baseUri)
{
	Transport = new HttpTransport(httpClient)
};
```

### Throttle Handling

The client automatically retries on HTTP 429 responses up to `MaxWaitOnThrottleMs` total:

```csharp
var client = new FiixCmmsClient(credentials, baseUri)
{
	MaxWaitOnThrottleMs = 10000 // default: 5000ms
};
```

## Error Handling

Every response derives from `Response` and exposes `ApiError? Error`. Always check it before accessing result properties:

```csharp
var response = await client.FindByIdAsync(request);

if (response.Error != null)
{
	Console.WriteLine($"Leg:     {response.Error.Leg}");
	Console.WriteLine($"Code:    {response.Error.Code}");
	Console.WriteLine($"Message: {response.Error.Message}");
	return;
}

// Safe to use response.Object here
```

| Property | Type | Description |
|----------|------|-------------|
| `Leg` | `string?` | Where in the pipeline the error occurred |
| `Code` | `int` | Numeric error code (429 = throttled) |
| `Message` | `string?` | Human-readable description |
| `Object` | `Dictionary<string, object>?` | Additional error metadata |

## Available Models

107 DTO classes covering all major Fiix CMMS entities, all inheriting from `ClientCmmsDto`:

| Category | Models |
|----------|--------|
| Assets | `Asset`, `AssetCategory`, `AssetClassification`, `AssetResolved`, … |
| Work Orders | `WorkOrder`, `WorkOrderStatus`, `WorkOrderTask`, `WorkOrderPart`, … |
| Maintenance | `ScheduledMaintenance`, `MaintenanceType`, `MeterReading`, … |
| Purchasing | `PurchaseOrder`, `PurchaseOrderLineItem`, `Receipt`, `Stock`, … |
| Users | `User`, `Business`, `RegionUser`, `SiteUser`, … |
| Calendar | `CalendarEvent`, `Dashboard`, `DashboardWidget`, … |

All DTOs expose `Dictionary<string, object>? ExtraFields` for API fields not yet modelled and for `dv_`-prefixed display values.

### Field Naming Convention

The Fiix API uses Hungarian notation. .NET properties use PascalCase and are serialized back to their original form by `FiixPropertyNamingPolicy`:

| Prefix | Type | JSON field | .NET property |
|--------|------|------------|---------------|
| `int` | Integer | `intSiteID` | `IntSiteID` |
| `str` | String | `strName` | `StrName` |
| `dtt` | DateTime | `dttDateCreated` | `DttDateCreated` |
| `bol` | Boolean | `bolIsActive` | `BolIsActive` |
| `dbl` | Double | `dblHourlyRate` | `DblHourlyRate` |
| `qty` | Quantity | `qtyOnHand` | `QtyOnHand` |

### Display Values

Request `dv_`-prefixed fields to get human-readable names for foreign-key fields:

```csharp
request.Fields = "id, strName, intCategoryID, dv_intCategoryID";

// asset.IntCategoryID                   => 5
// asset.ExtraFields["dv_intCategoryID"] => "Pumps"
```

## Architecture

```
FiixCmms.Client/
├── FiixCmmsClient.cs          Main client facade
├── BasicCredentials.cs        ICredentials implementation
├── Api/
│   ├── Request.cs             Base request class
│   ├── Response.cs            Base response class (ApiError? Error)
│   ├── ApiError.cs            Error details
│   ├── Crud/
│   │   ├── FindByIdRequest/Response
│   │   ├── FindRequest/Response   (includes FindFilter, TotalObjects)
│   │   ├── AddRequest/Response
│   │   ├── ChangeRequest/Response (includes ChangeFields)
│   │   └── RemoveRequest/Response
│   ├── Rpc/
│   │   ├── RpcRequest/Response
│   │   ├── ParameterizedRpcRequest/Response<T>
│   │   └── PagedResponse<T>
│   └── Batch/
│       ├── BatchRequest       List<Request> Requests
│       └── BatchResponse      List<Response> Responses
├── Models/                    107 DTO classes + ClientCmmsDto base
├── Interfaces/                ICredentials, ITransport, IFormat
├── Transport/                 HttpTransport (injectable)
├── Format/                    JsonFormat + FiixPropertyNamingPolicy
├── Diagnostics/               SerializationDiagnostics
└── Helpers/                   UrlEncodingHelper
```

**Requirements:** .NET 10 · `System.Text.Json` (inbox) · `System.Net.Http` (inbox)

**Resources:**
- [Official Fiix API Documentation](https://fiixlabs.github.io/api-documentation/)
- [API Reference](https://fiixlabs.github.io/api-documentation/index.html#/ApiDoc)
- [Fiix Support](https://www.fiixsoftware.com/support)

## License

This project is licensed under the **Apache License 2.0**. See the [LICENSE](../LICENSE) file for the full text.
