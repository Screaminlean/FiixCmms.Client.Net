namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'File' table.
/// </summary>
public class File : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? IntFileContentsID { get; set; }
    public long? IntFileTypeID { get; set; }
    public long? IntSize { get; set; }
    public long? IntWorkOrderID { get; set; }
    public string? StrLink { get; set; }
    public string? StrName { get; set; }
    public string? StrNotes { get; set; }
}

