using System.Text.Json;
using FiixCmms.MockApi.Auth;
using FiixCmms.MockApi.Handlers;
using FiixCmms.MockApi.Store;

var builder = WebApplication.CreateBuilder(args);

// Bind mock credentials from configuration
var credentials = builder.Configuration
    .GetSection("MockCredentials")
    .Get<MockCredentials>() ?? new MockCredentials();

var app = builder.Build();

// Single endpoint matching the Fiix API contract:
// POST baseUri?action=TypeName&service=cmms&accessKey=...&appKey=...&timestamp=...
app.MapPost("/api/", async (HttpRequest request) =>
{
    // --- HMAC-SHA256 signature validation ---
    var authError = SignatureValidator.Validate(request, credentials);
    if (authError != null)
    {
        app.Logger.LogWarning("Auth rejected: {Error}", authError);
        return Results.Text(JsonSerializer.Serialize(new
        {
            error = new { code = 403, message = $"Authentication failed: {authError}" }
        }), "text/plain");
    }

    var action = request.Query["action"].ToString();

    // Strip generic arity suffix: "FindRequest`1" -> "FindRequest"
    var backtickIdx = action.IndexOf('`');
    if (backtickIdx >= 0) action = action[..backtickIdx];

    var body = await new StreamReader(request.Body).ReadToEndAsync();

    JsonElement doc;
    try
    {
        doc = JsonSerializer.Deserialize<JsonElement>(body);
    }
    catch
    {
        return Results.Text(JsonSerializer.Serialize(new { error = new { code = -1, message = "Invalid JSON body" } }), "text/plain");
    }

    var responseJson = action switch
    {
        "FindRequest"               => CrudHandler.Find(doc),
        "FindByIdRequest"           => CrudHandler.FindById(doc),
        "AddRequest"                => CrudHandler.Add(doc),
        "ChangeRequest"             => CrudHandler.Change(doc),
        "RemoveRequest"             => CrudHandler.Remove(doc),
        "RpcRequest"                => RpcHandler.Handle(doc),
        "ParameterizedRpcRequest"   => RpcHandler.HandleParameterized(doc),
        "BatchRequest"              => BatchHandler.Handle(doc),
        _ => JsonSerializer.Serialize(new
        {
            error = new { code = -1, message = $"Unknown action: {action}" }
        })
    };

    return Results.Text(responseJson, "text/plain");
});

app.Logger.LogInformation("Fiix Mock API running. Configure CLI with BaseUri: http://localhost:5100/api/");
app.Run();

