# FiixCmms.Client.Net

A .NET 10 client library for the [Fiix CMMS API](https://fiixlabs.github.io/api-documentation/), ported from the official Java client. Supports full CRUD, RPC, and Batch operations with async/await throughout.

## Table of Contents

- [Overview](#overview)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Configuration](#configuration)
- [Quick Start](#quick-start)
- [CRUD Operations](#crud-operations)
- [RPC Methods](#rpc-methods)
- [Batch Operations](#batch-operations)
- [Error Handling](#error-handling)
- [Field Naming Convention](#field-naming-convention)
- [Display Values](#display-values)
- [Design Philosophy](#design-philosophy)
- [CLI Testing Tool](#cli-testing-tool)
- [API Compatibility Notes](#api-compatibility-notes)
- [Resources](#resources)
- [License](#license)

---

## Overview

**API Version:** 2.48.1  
**Target Framework:** .NET 10 (`net10.0`)  
**Serialization:** System.Text.Json  
**Authentication:** HMAC SHA256

### API Coverage

| API Family | Coverage | Operations |
|------------|----------|------------|
| **CRUD**   | ✅ 100%  | Add, Find, FindById, Change, Remove |
| **RPC**    | ✅ 100%  | All 18 documented methods |
| **Batch**  | ✅ 100%  | Atomic multi-operation execution |

---

## Project Structure

```
FiixCmms.Client.Net/
├── FiixCmms.Client/                  # Core library
│   ├── Api/
│   │   ├── Batch/                    # Batch request/response types
│   │   ├── Crud/                     # CRUD request/response types
│   │   │   ├── FindFilter.cs         # Query language filter
│   │   │   ├── FindRequest.cs / FindResponse.cs
│   │   │   ├── FindByIdRequest.cs / FindByIdResponse.cs
│   │   │   ├── AddRequest.cs / AddResponse.cs
│   │   │   ├── ChangeRequest.cs / ChangeResponse.cs
│   │   │   └── RemoveRequest.cs / RemoveResponse.cs
│   │   ├── Rpc/                      # RPC request/response types
│   │   ├── Request.cs / Response.cs  # Base classes
│   │   └── ApiError.cs
│   ├── Format/
│   │   └── JsonFormat.cs             # JSON serialization with Fiix naming policy
│   ├── Interfaces/
│   │   ├── ICredentials.cs
│   │   ├── IFormat.cs
│   │   └── ITransport.cs
│   ├── Models/                       # 107 DTO classes
│   │   ├── ClientCmmsDto.cs          # Base DTO with ExtraFields support
│   │   ├── Asset.cs
│   │   ├── WorkOrder.cs
│   │   ├── User.cs
│   │   └── ... (104 more)
│   ├── Transport/
│   │   └── HttpTransport.cs
│   ├── BasicCredentials.cs
│   └── FiixCmmsClient.cs             # Main client facade
│
└── FiixCmms.Client.Cli/              # CLI testing tool
    ├── Examples/
    │   ├── Rpc/
    │   │   ├── AllRpcMethods.cs      # Helpers for all 18 RPC methods
    │   │   └── ComprehensiveRpcExamples.cs
    │   ├── CrudExample.cs
    │   ├── FindExamples.cs
    │   ├── BatchExamples.cs
    │   ├── RpcExamples.cs
    │   ├── TestConnection.cs
    │   └── DiagnosticsExample.cs
    ├── Program.cs
    ├── FiixCmmsSettings.cs
    └── appsettings.json
```

---

## Installation

Add a project reference to `FiixCmms.Client`:

```bash
dotnet add reference ../FiixCmms.Client/FiixCmms.Client.csproj
```

---

## Configuration

Create `appsettings.local.json` in the CLI project (or configure programmatically):

```json
{
  "FiixCmms": {
    "BaseUri": "https://yourinstance.fiixsandbox.com/api/",
    "AppKey": "your-app-key",
    "AccessKey": "your-access-key",
    "SecretKey": "your-secret-key"
  }
}
```

---

## Quick Start

```csharp
using FiixCmms.Client;
using FiixCmms.Client.Models;

var credentials = new BasicCredentials(
    appKey: "your-app-key",
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
}
```

---

## CRUD Operations

### Find by ID

```csharp
var request = client.PrepareFindById<Asset>();
request.Id = 12345;
request.Fields = "id, strName, strCode";

var response = await client.FindByIdAsync(request);
if (response.Error == null)
{
    var asset = response.Object;
    Console.WriteLine($"{asset.StrName} ({asset.StrCode})");
}
```

**Response:** `{ "object": { ... } }`

### Find with Filters

```csharp
var request = client.PrepareFind<WorkOrder>();
request.Filters = new List<FindFilter>
{
    new FindFilter
    {
        Ql = "intWorkOrderStatusID=? AND bolIsActive=?",
        Parameters = new List<object> { 1, 1L }
    }
};
request.Fields = "id, strCode, strDescription";
request.OrderBy = "id DESC";
request.Limit = 10;

var response = await client.FindAsync(request);
foreach (var wo in response.Objects ?? new List<WorkOrder>())
{
    Console.WriteLine($"{wo.StrCode}: {wo.StrDescription}");
}
```

**Response:** `{ "objects": [ ... ] }`

#### Filter Query Language

The `Ql` property uses `?` placeholders with `Parameters`:

```csharp
// Equality
{ Ql = "intSiteID = ?", Parameters = new List<object> { 1 } }

// Multiple conditions
{ Ql = "intSiteID = ? AND bolIsActive = ?", Parameters = new List<object> { 1, 1L } }

// String match
{ Ql = "strName LIKE ?", Parameters = new List<object> { "%pump%" } }
```

**Supported Operators:** `=`, `!=`, `>`, `<`, `>=`, `<=`, `LIKE`, `AND`, `OR`, `IS NULL`, `IS NOT NULL`

### Add (Create)

```csharp
var request = client.PrepareAdd<Asset>();
request.Object = new Asset
{
    StrName = "New Equipment",
    StrCode = "EQ-001",
    IntSiteID = 1
};
request.Fields = "id, strName, strCode";

var response = await client.AddAsync(request);
Console.WriteLine($"Created ID: {response.Object?.Id}");
```

**Response:** `{ "object": { ... } }`

### Change (Update)

```csharp
var request = client.PrepareChange<Asset>();
request.Object = new Asset
{
    Id = 12345,
    StrName = "Updated Name",
    StrDescription = "Updated Description"
};
request.ChangeFields = "strName, strDescription"; // Only these fields are written
request.Fields = "id, strName, strDescription";    // Fields returned in response

var response = await client.ChangeAsync(request);
```

> **Important:** Only fields listed in `ChangeFields` are updated, even if other properties are set on `Object`.

**Response:** `{ "object": { ... } }`

### Remove (Delete)

```csharp
var request = client.PrepareRemove<Asset>();
request.Id = 12345;

var response = await client.RemoveAsync(request);
Console.WriteLine($"Removed {response.Count} record(s)");
```

**Response:** `{ "success": true }`

---

## RPC Methods

All 18 documented Fiix RPC methods are implemented.

### Basic RPC (no parameters)

```csharp
var request = client.PrepareRpc();
request.Name = "Ping";

var response = await client.RpcAsync(request);
if (response.Error == null)
{
    Console.WriteLine("Server is up!");
}
```

### Parameterized RPC

```csharp
var request = client.PrepareParameterizedRpc();
request.Name = "CustomFields";
request.Action = "getWorkOrderCustomFieldsMetaData";
// request.Parameters = new Dictionary<string, object> { ... };

var response = await client.RpcAsync<dynamic>(request);
```

### Available RPC Methods

| Category | Method | Description |
|----------|--------|-------------|
| Connectivity | `Ping` | Test server connectivity |
| Connectivity | `GetTimezone` | Get server timezone |
| Connectivity | `GetAccessibleSites` | Sites accessible to current user |
| Assets | `AssetResolved` | Resolved asset info with related data |
| Calendar | `Calendar` | Calendar information |
| Scheduling | `ScheduleTriggerAssetEvent` | Asset event scheduling |
| Scheduling | `ScheduleTriggerCommon` | Common schedule trigger |
| Scheduling | `ScheduleTriggerMeterReading` | Meter reading trigger |
| Scheduling | `ScheduleTriggerTime` | Time-based trigger |
| Work Orders | `WorkOrderLog` | Work order log entries |
| Work Orders | `FollowOnWorkOrders` | Follow-on work orders |
| Work Orders | `TaskGroupsToWorkOrder` | Convert task groups to WOs |
| Custom Fields | `CustomFields` | Custom field operations |
| Inventory | `StocksReceived` | Received stock information |
| Audit | `ActivityLog` | Activity log entries |
| Audit | `AuditTrail` | Audit trail information |
| Reporting | `DashboardWidget` | Dashboard widget data |
| Reporting | `DataExport` | Data export |

### RPC Client Methods

```csharp
// Basic RPC
public async Task<RpcResponse> RpcAsync(RpcRequest request, CancellationToken ct = default)

// Parameterized RPC
public async Task<ParameterizedRpcResponse<T>> RpcAsync<T>(ParameterizedRpcRequest request, CancellationToken ct = default) where T : class

// Paged RPC
public async Task<PagedResponse<T>> RpcPagedAsync<T>(RpcRequest request, CancellationToken ct = default) where T : class
```

---

## Batch Operations

Execute multiple CRUD and/or RPC operations in a single HTTP request. The entire batch is transactional — if any operation fails, all are rolled back.

### Basic Batch

```csharp
var batch = client.PrepareBatch();

// Add multiple operations
var assetReq = client.PrepareFind<Asset>();
assetReq.Fields = "id, strName";
assetReq.Limit = 10;
batch.Requests.Add(assetReq);

var pingReq = client.PrepareRpc();
pingReq.Name = "Ping";
batch.Requests.Add(pingReq);

var response = await client.BatchAsync(batch);

if (response.Error == null)
{
    // response.Responses[0] = Asset find results
    // response.Responses[1] = Ping result
    Console.WriteLine($"Got {response.Responses.Count} responses");
}
```

### Transactional Creates

```csharp
var batch = client.PrepareBatch();

for (int i = 1; i <= 3; i++)
{
    var addReq = client.PrepareAdd<Asset>();
    addReq.Object = new Asset { StrName = $"Asset {i}", IntSiteID = siteId };
    addReq.Fields = "id, strName";
    batch.Requests.Add(addReq);
}

// All three assets are created, or none are (transactional)
var response = await client.BatchAsync(batch);
```

### Performance

| Approach | Requests | Time (est.) |
|----------|----------|-------------|
| 4 individual calls | 4 | ~600ms |
| 1 batch call | 1 | ~200ms (~67% faster) |

### Best Practices

- Recommended batch size: 10–50 operations
- Split unrelated operations into separate batches to limit rollback scope
- Track response ordering — responses match request order exactly

---

## Error Handling

All responses include an `Error` property:

```csharp
var response = await client.FindByIdAsync(request);

if (response.Error != null)
{
    Console.WriteLine($"Error: {response.Error.Message}");
    Console.WriteLine($"Code:  {response.Error.Code}");
    Console.WriteLine($"Leg:   {response.Error.Leg}");
    return;
}

// Safe to use response.Object here
```

---

## Field Naming Convention

The Fiix API uses Hungarian notation. Properties in the .NET DTOs use PascalCase and are serialized back to their original casing via `FiixPropertyNamingPolicy` (lowercase first character, rest preserved):

| Prefix | Type | Example field | .NET Property |
|--------|------|---------------|---------------|
| `int`  | Integer | `intSiteID` | `IntSiteID` |
| `str`  | String | `strName` | `StrName` |
| `dtt`  | DateTime | `dttDateCreated` | `DttDateCreated` |
| `bol`  | Boolean | `bolIsActive` | `BolIsActive` |
| `dbl`  | Double | `dblHourlyRate` | `DblHourlyRate` |
| `qty`  | Quantity | `qtyOnHand` | `QtyOnHand` |

---

## Display Values

Request `dv_`-prefixed fields to receive human-readable display values for foreign-key fields. They are returned in `ExtraFields` on the DTO:

```csharp
request.Fields = "id, strName, intCategoryID, dv_intCategoryID";

// Response:
// asset.IntCategoryID          => 5
// asset.ExtraFields["dv_intCategoryID"] => "Pumps"
```

---

## Design Philosophy

### Async-Only API

All network methods are `async` — there are no synchronous equivalents. This matches modern .NET libraries (HttpClient, Azure SDK, EF Core) and avoids deadlocks in UI and ASP.NET contexts.

```csharp
// ✅ Correct
public async Task<List<Asset>> GetAssetsAsync()
{
    var request = _client.PrepareFind<Asset>();
    var response = await _client.FindAsync(request);
    return response.Objects ?? new List<Asset>();
}

// ❌ Avoid — can deadlock
var response = _client.FindAsync(request).Result;
```

For legacy synchronous contexts only:

```csharp
var response = client.FindAsync(request).GetAwaiter().GetResult();
```

See: [Don't Block on Async Code](https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html)

### Dependency Injection

`ITransport` and `IFormat` are swappable for testing:

```csharp
var transport = new HttpTransport(customHttpClient);
client.Transport = transport;
```

---

## CLI Testing Tool

The `FiixCmms.Client.Cli` project provides a command-line tool for exploring the API.

### Setup

```bash
cd FiixCmms.Client.Cli
# Create appsettings.local.json with your credentials (see Configuration above)
```

### Commands

```bash
# Authentication
dotnet run -- test-auth

# CRUD
dotnet run -- crud                     # Complete CRUD workflow
dotnet run -- find Asset               # Find all assets
dotnet run -- find Asset 12345         # Find asset by ID
dotnet run -- add                      # Add entity
dotnet run -- change                   # Update entity
dotnet run -- remove Asset 12345       # Delete entity

# RPC
dotnet run -- rpc                      # Basic RPC examples
dotnet run -- rpc ping                 # Test connectivity
dotnet run -- rpc timezone             # Get server timezone
dotnet run -- rpc sites                # Get accessible sites
dotnet run -- rpc custom-fields        # Custom fields example
dotnet run -- rpc comprehensive        # Test all 18 RPC methods

# Batch
dotnet run -- batch                    # All batch examples
dotnet run -- batch find               # Batch find multiple entity types
dotnet run -- batch add 1              # Batch create assets (transactional, site ID = 1)
dotnet run -- batch mixed              # Mixed CRUD + RPC batch

# Diagnostics
dotnet run -- diagnostics              # Serialization diagnostics
dotnet run -- help                     # Show all commands
```

---

## API Compatibility Notes

The implementation has been verified against the official Fiix API documentation (v2.48.1). The following items are confirmed correct:

| Feature | Status |
|---------|--------|
| `FindFilter` with Ql query language | ✅ Verified |
| `Filter.Parameters` list | ✅ Verified |
| `Fields` as comma-separated string | ✅ Verified |
| `OrderBy` support | ✅ Verified |
| `ChangeFields` parameter | ✅ Verified |
| Singular `Object` in Add/Change requests | ✅ Verified |
| Singular `Id` in Remove request | ✅ Verified |
| `Response.Object` / `Response.Objects` | ✅ Verified |
| Hungarian notation field naming | ✅ Verified |
| HMAC SHA256 authentication | ✅ Verified |
| `ExtraFields` for display values | ✅ Verified |

### Not Yet Implemented (Optional)

- Batch request support for callback-style patterns (the .NET client uses async/await instead — preferred)
- Synchronous wrapper methods (intentionally omitted; see Design Philosophy)

### Open Items to Verify Against a Live Instance

- Date/time serialization edge cases
- Null value handling in responses
- API-specific error codes

---

## Resources

- [Official Fiix API Documentation](https://fiixlabs.github.io/api-documentation/)
- [Developer Guide](https://fiixlabs.github.io/api-documentation/guide.html)
- [API Reference](https://fiixlabs.github.io/api-documentation/index.html#/ApiDoc)
- [Fiix Support](https://www.fiixsoftware.com/support)

---

## License

This project is licensed under the **Apache License 2.0**. See the [LICENSE](LICENSE) file for the full text.
