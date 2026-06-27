namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'FileContents' table.
/// </summary>
public partial class FileContents : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntSysCode { get; set; }
    public long? IntIsShared { get; set; }
    public long? IntSize { get; set; }
    public string? StrContents { get; set; }
    public string? StrMimeType { get; set; }
    public string? StrName { get; set; }
}

