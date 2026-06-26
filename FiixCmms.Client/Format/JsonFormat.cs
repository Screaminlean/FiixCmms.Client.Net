using System.Text.Json;
using System.Text.Json.Serialization;
using FiixCmms.Client.Interfaces;

namespace FiixCmms.Client.Format;

/// <summary>
/// JSON implementation of the IFormat interface using System.Text.Json.
/// Handles serialization and deserialization of requests and responses.
/// NOTE: Property naming strategy matches Fiix API expectations (camelCase for first char, preserve rest).
/// </summary>
public class JsonFormat : IFormat
{
    private readonly JsonSerializerOptions _options;

    /// <summary>
    /// Initializes a new instance of the JsonFormat class with default JSON serialization options.
    /// </summary>
    public JsonFormat()
    {
        _options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = new FiixPropertyNamingPolicy(),
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
    }

    /// <summary>
    /// Initializes a new instance with custom serialization options.
    /// </summary>
    public JsonFormat(JsonSerializerOptions options)
    {
        _options = options;
    }

    /// <inheritdoc/>
    public string RequestToString(object request)
    {
        return JsonSerializer.Serialize(request, _options);
    }

    /// <inheritdoc/>
    public T StringToRequest<T>(string str)
    {
        var result = JsonSerializer.Deserialize<T>(str, _options);
        return result ?? throw new InvalidOperationException("Deserialization returned null");
    }

    /// <inheritdoc/>
    public string ResponseToString(object response)
    {
        return JsonSerializer.Serialize(response, _options);
    }

    /// <inheritdoc/>
    public T StringToResponse<T>(string str)
    {
        var result = JsonSerializer.Deserialize<T>(str, _options);
        return result ?? throw new InvalidOperationException("Deserialization returned null");
    }
}

/// <summary>
/// Custom naming policy for Fiix API that preserves field name casing.
/// Converts PascalCase properties to match original Java field names:
/// - StrName → strName
/// - IntSiteID → intSiteID (not intSiteId)
/// - BolIsOnline → bolIsOnline
/// </summary>
internal class FiixPropertyNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length == 0)
            return name;

        // Simply lowercase the first character, preserve the rest
        // This matches the Java field naming convention
        return char.ToLowerInvariant(name[0]) + name.Substring(1);
    }
}
