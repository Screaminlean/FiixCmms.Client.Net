# Fiix CMMS Mock API

A local ASP.NET minimal API that simulates the Fiix CMMS JSON-RPC endpoint.
Lets you run all CLI examples and test the client library **without a real Fiix account**.

## Purpose

- Zero-credential local development and testing
- Exercises the full client library stack (serialization, signing, request/response shapes)
- Serves as a living contract spec for the API

## Running

```bash
dotnet run --project FiixCmms.MockApi
```

Starts on **http://localhost:5100** — the same URL baked into the CLI's default `appsettings.json`.

## What It Simulates

| Feature | Coverage |
|---------|----------|
| CRUD — Find, FindById, Add, Change, Remove | ✅ Full in-memory store |
| Filter (`intSysCode=?`, `strShort2=?`, `intCategoryID=? AND bolIsSite=?`) | ✅ Simple Ql parser |
| Field projection (`fields = "id, strName, ..."`) | ✅ |
| Display-value fields (`dv_intCountryID`) | ✅ Returns mock string |
| RPC — Ping, GetTimezone, GetAccessibleSites, CustomFields | ✅ |
| All other RPC methods | ✅ Returns empty mock data |
| Batch requests (find, add, mixed) | ✅ |
| HMAC signature validation | ✅ Validates all requests against configured credentials |

## Seed Data

The store is pre-seeded with:

| Type | Records |
|------|---------|
| `AssetCategory` | Locations and Facilities (sysCode=1), Equipment (sysCode=2) |
| `Country` | Canada (strShort2=CA), United States (strShort2=US) |
| `Asset` | Main Plant, Warehouse A, Pump Station 1 |
| `WorkOrder` | 2 sample work orders |
| `User` | 2 sample users |

## Using with the CLI

The CLI's `appsettings.json` already points at the mock by default:

```json
{
  "FiixCmms": {
	"BaseUri": "http://localhost:5100/api/",
	"AppKey": "mock-app-key",
	"AccessKey": "mock-access-key",
	"SecretKey": "mock-secret-key"
  }
}
```

**Terminal 1 — start the mock:**
```bash
dotnet run --project FiixCmms.MockApi
```

**Terminal 2 — run CLI commands:**
```bash
dotnet run --project FiixCmms.Client.Cli -- crud
dotnet run --project FiixCmms.Client.Cli -- rpc
dotnet run --project FiixCmms.Client.Cli -- batch find
dotnet run --project FiixCmms.Client.Cli -- find Asset
```

## Using with a Real Fiix Instance

Create `FiixCmms.Client.Cli/appsettings.local.json` with your real credentials:

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

The local file overrides the mock URL automatically — no code changes needed.

## License

Apache License 2.0 — see [LICENSE](../LICENSE).
