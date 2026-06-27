using System.Security.Cryptography;
using System.Text;

namespace FiixCmms.MockApi.Auth;

/// <summary>
/// Validates the HMAC-SHA256 request signature sent by the Fiix client library.
///
/// The client signs the full URI (scheme stripped) using the SecretKey as the HMAC key
/// and sends the hex-encoded result in the Authorization header.
///
/// Algorithm (mirrors FiixCmmsClient.CalculateSignature):
///   message = uri stripped of "http://" or "https://"
///   key     = SecretKey (UTF-8 bytes)
///   digest  = HMAC-SHA256(key, message)
///   result  = lowercase hex string (no dashes)
/// </summary>
public static class SignatureValidator
{
    /// <summary>
    /// Validates the Authorization header signature against the expected value.
    /// Returns a descriptive error string on failure, or null on success.
    /// </summary>
    public static string? Validate(HttpRequest request, MockCredentials credentials)
    {
        // --- 1. Check credentials in query string ---
        var appKey    = request.Query["appKey"].ToString();
        var accessKey = request.Query["accessKey"].ToString();

        if (appKey != credentials.AppKey)
            return $"Invalid appKey. Expected '{credentials.AppKey}', got '{appKey}'.";

        if (accessKey != credentials.AccessKey)
            return $"Invalid accessKey. Expected '{credentials.AccessKey}', got '{accessKey}'.";

        // --- 2. Check Authorization header ---
        if (!request.Headers.TryGetValue("Authorization", out var authValues) ||
            string.IsNullOrEmpty(authValues))
            return "Missing Authorization header.";

        var receivedSignature = authValues.ToString();

        // --- 3. Reconstruct the signed URI the client used ---
        // The client signs: scheme-stripped full URI including query string
        // e.g. "localhost:5100/api/?action=...&appKey=...&..."
        var fullUri = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        var message = fullUri.StartsWith("http://")  ? fullUri["http://".Length..]
                    : fullUri.StartsWith("https://") ? fullUri["https://".Length..]
                    : fullUri;

        // --- 4. Compute expected signature ---
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var keyBytes     = Encoding.UTF8.GetBytes(credentials.SecretKey);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes  = hmac.ComputeHash(messageBytes);
        var expected   = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

        // --- 5. Compare ---
        if (!CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(receivedSignature),
                Encoding.UTF8.GetBytes(expected)))
        {
            return $"Signature mismatch. Received: {receivedSignature}";
        }

        return null; // valid
    }
}

/// <summary>
/// Strongly-typed credentials bound from appsettings MockCredentials section.
/// </summary>
public class MockCredentials
{
    public string AppKey    { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}
