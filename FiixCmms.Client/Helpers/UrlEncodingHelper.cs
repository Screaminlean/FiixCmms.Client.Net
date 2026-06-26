namespace FiixCmms.Client.Helpers;

/// <summary>
/// Utility class for JavaScript-compatible UTF-8 encoding and decoding.
/// Provides methods that produce output compatible with JavaScript's encodeURIComponent function.
/// </summary>
public static class UrlEncodingHelper
{
    /// <summary>
    /// Decodes the passed UTF-8 string using an algorithm compatible with
    /// JavaScript's decodeURIComponent function.
    /// </summary>
    /// <param name="s">The UTF-8 encoded string to be decoded.</param>
    /// <returns>The decoded string, or null if the input is null.</returns>
    public static string? DecodeURIComponent(string? s)
    {
        if (s == null)
        {
            return null;
        }

        return Uri.UnescapeDataString(s);
    }

    /// <summary>
    /// Encodes the passed string as UTF-8 using an algorithm compatible with
    /// JavaScript's encodeURIComponent function.
    /// </summary>
    /// <param name="s">The string to be encoded.</param>
    /// <returns>The encoded string, or null if the input is null.</returns>
    public static string? EncodeURIComponent(string? s)
    {
        if (s == null)
        {
            return null;
        }

        // Uri.EscapeDataString is similar to JavaScript's encodeURIComponent
        // but we need to make some adjustments for exact compatibility
        var encoded = Uri.EscapeDataString(s);

        // Make it compatible with JavaScript's encodeURIComponent
        encoded = encoded
            .Replace("!", "%21")
            .Replace("'", "%27")
            .Replace("(", "%28")
            .Replace(")", "%29")
            .Replace("~", "%7E");

        return encoded;
    }
}
