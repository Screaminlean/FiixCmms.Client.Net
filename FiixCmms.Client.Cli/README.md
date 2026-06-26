# Fiix CMMS Client - Command Line Testing Tool

A command-line tool for testing and demonstrating the Fiix CMMS .NET Client library.

## Purpose

This tool allows you to:
- Test your Fiix CMMS API credentials
- Run CRUD, RPC, and Batch operations against your Fiix instance
- Validate the .NET client implementation
- Learn by example

## Configuration

### Option 1: appsettings.local.json (Recommended)

Create `appsettings.local.json` in the project directory with your credentials:

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

> ⚠️ This file is in `.gitignore` and will NOT be committed.

### Option 2: Environment Variables

```
FiixCmms__BaseUri
FiixCmms__AppKey
FiixCmms__AccessKey
FiixCmms__SecretKey
```

---

## Usage

Run all commands from the `FiixCmms.Client.Cli` project directory with `dotnet run -- <command>`.

### Test Connection
```bash
dotnet run -- test-auth
```

### Complete CRUD Example
```bash
dotnet run -- crud
```

Runs the full CRUD workflow matching the official Fiix Java examples:
1. Find the "Locations" asset category
2. Create an asset assigned to that category
3. Find a country by short name
4. Update the asset
5. Find the asset by ID
6. List all location assets
7. Delete the asset

### Find Operations

```bash
# Find first 10 assets
dotnet run -- find Asset

# Find a specific asset by ID
dotnet run -- find Asset 12345

# Find a specific work order by ID
dotnet run -- find WorkOrder 67890

# Find all users
dotnet run -- find User
```

> Supported entity types: `Asset`, `WorkOrder`, `User`

### Remove

```bash
dotnet run -- remove Asset 12345
```

Prompts for confirmation (`yes/no`) before deleting. ⚠️ Permanently deletes the entity.

### RPC Operations

```bash
# Run basic RPC examples (ping, timezone, sites)
dotnet run -- rpc

# Test server connectivity
dotnet run -- rpc ping

# Get server timezone
dotnet run -- rpc timezone

# Get accessible sites
dotnet run -- rpc sites

# Custom fields RPC example
dotnet run -- rpc custom-fields

# Test all 18 documented RPC methods
dotnet run -- rpc comprehensive
dotnet run -- rpc comp         # alias
```

### Batch Operations

```bash
# Run all batch examples
dotnet run -- batch

# Batch find (Asset, WorkOrder, User in one request)
dotnet run -- batch find

# Batch add multiple assets atomically (requires site ID)
dotnet run -- batch add 1

# Mixed batch: RPC + CRUD in one request
dotnet run -- batch mixed
```

### Diagnostics

```bash
dotnet run -- diagnostics
```

Runs serialization tests to verify the JSON payload matches Fiix API expectations. Automatically performs a live request signing test if real credentials are configured.

### Help

```bash
dotnet run -- help
```

---

## Available Commands

| Command | Description |
|---------|-------------|
| `crud` | Complete CRUD workflow (matching official Java examples) |
| `find <type> [id]` | Find entities by type. Types: `Asset`, `WorkOrder`, `User`. Omit ID to list first 10. |
| `remove <type> <id>` | Remove entity by ID (prompts for confirmation) |
| `rpc` | Run basic RPC examples (ping, timezone, sites) |
| `rpc ping` | Test server connectivity |
| `rpc timezone` | Get server timezone |
| `rpc sites` | Get accessible sites |
| `rpc custom-fields` | Custom fields RPC example |
| `rpc comprehensive` | Test all 18 documented RPC methods |
| `batch` | Run all batch examples |
| `batch find` | Batch find multiple entity types in one request |
| `batch add <site-id>` | Batch create 3 assets atomically |
| `batch mixed` | Mixed batch (RPC + CRUD in one request) |
| `test-auth` | Test authentication and connection |
| `diagnostics` | Run serialization diagnostics |
| `help` | Show help |

> **Note:** `add` and `change` commands are not yet implemented as standalone commands. Use `crud` to see full add/change examples.

---

## Building

```bash
# Build
dotnet build

# Run directly
dotnet run -- <command>

# Build release and run the binary
dotnet build -c Release
./bin/Release/net10.0/FiixCmms.Client.Cli test-auth
```

---

## Security Notes

- **Never commit credentials** to source control
- Use `appsettings.local.json` for local testing (it's in `.gitignore`)
- Use environment variables in CI/CD pipelines
- Always use a **sandbox instance** for testing, not production

---

## Troubleshooting

| Problem | Solution |
|---------|----------|
| Configuration not found | Ensure `appsettings.local.json` exists with valid credentials, or set environment variables |
| Connection failed | Verify `BaseUri` is correct (must end with `/api/`) and you have network access |
| Authentication failed | Check `AppKey`, `AccessKey`, and `SecretKey` are correct and still active |
| Permission denied | Ensure your API key has the required permissions — check with your Fiix administrator |

---

## Extending

To add a new example command:

1. Create a new file in `Examples/`
2. Add a `case` in `Program.cs`

```csharp
// Examples/MyExample.cs
public static class MyExample
{
	public static async Task RunAsync(FiixCmmsSettings settings)
	{
		var credentials = new BasicCredentials(settings.AppKey, settings.AccessKey, settings.SecretKey);
		var client = new FiixCmmsClient(credentials, settings.BaseUri);
		// ...
	}
}

// Program.cs — add inside the switch block
case "my-command":
	await MyExample.RunAsync(settings);
	break;
```

---

## Related

- [`../FiixCmms.Client/README.md`](../FiixCmms.Client/README.md) — Library documentation
- [`../README.md`](../README.md) — Repository overview
- [Official Fiix API Documentation](https://fiixlabs.github.io/api-documentation/)

## License

This project is licensed under the **Apache License 2.0**. See the [LICENSE](../LICENSE) file for the full text.
