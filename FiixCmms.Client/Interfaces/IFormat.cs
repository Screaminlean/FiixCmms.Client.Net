namespace FiixCmms.Client.Interfaces;

/// <summary>
/// Interface for serialization format handlers that convert requests and responses
/// to and from string representations (typically JSON).
/// </summary>
public interface IFormat
{
    /// <summary>
    /// Converts a request object to its string representation.
    /// </summary>
    /// <param name="request">The request object to serialize.</param>
    /// <returns>The serialized string representation of the request.</returns>
    string RequestToString(object request);

    /// <summary>
    /// Converts a string to a request object of the specified type.
    /// </summary>
    /// <typeparam name="T">The target request type.</typeparam>
    /// <param name="str">The string to deserialize.</param>
    /// <returns>The deserialized request object.</returns>
    T StringToRequest<T>(string str);

    /// <summary>
    /// Converts a response object to its string representation.
    /// </summary>
    /// <param name="response">The response object to serialize.</param>
    /// <returns>The serialized string representation of the response.</returns>
    string ResponseToString(object response);

    /// <summary>
    /// Converts a string to a response object of the specified type.
    /// </summary>
    /// <typeparam name="T">The target response type.</typeparam>
    /// <param name="str">The string to deserialize.</param>
    /// <returns>The deserialized response object.</returns>
    T StringToResponse<T>(string str);
}
