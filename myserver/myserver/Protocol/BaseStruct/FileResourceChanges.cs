using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class CreateFileOptions
{
    /// <summary>
    /// Overwrite existing file. Overwrite wins over `ignoreIfExists`
    /// </summary>
    public bool? overwrite { get; set; }
    public bool? ignoreIfExists { get; set; }
}

public class CreateFile
{
    public string kind => "create";
    public Uri uri { get; set; }
    public CreateFileOptions? options { get; set; }
    /// <summary>
    /// An optional annotation identifier
    /// </summary>
    public string? annotationId { get; set; }
}

public class RenameFileOptions
{
    /// <summary>
    /// Overwrite existing file. Overwrite wins over `ignoreIfExists`
    /// </summary>
    public bool? overwrite { get; set; }
    public bool? ignoreIfExists { get; set; }
}

public class RenameFile
{
    public string kind => "rename";
    public Uri oldUri { get; set; }
    public Uri newUri { get; set; }
    public RenameFileOptions? options { get; set; }
    /// <summary>
    /// An optional annotation identifier
    /// </summary>
    public string? annotationId { get; set; }
}

public class DeleteFileOptions
{
    /// <summary>
    /// Delete the content recursively if a folder is denoted.
    /// </summary>
    public bool? recursive { get; set; }
    public bool? ignoreIfNotExists { get; set; }
}

public class DeleteFile
{
    public string kind => "delete";
    public Uri uri { get; set; }
    public DeleteFileOptions? options { get; set; }
    /// <summary>
    /// An optional annotation identifier
    /// </summary>
    public string? annotationId { get; set; }
}

