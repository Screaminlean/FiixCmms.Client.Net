namespace FiixCmms.Client.Api.Batch;

/// <summary>
/// A concrete response type used to deserialize individual sub-responses within a <see cref="BatchResponse"/>.
/// Each sub-response mirrors the base <see cref="Response"/> shape (an optional error envelope).
/// </summary>
public sealed class SubResponse : Response
{
}
