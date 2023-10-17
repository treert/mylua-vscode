using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Protocol;
public class MarkdownClientCapabilities
{
    public string parser { get; set; }
    public string? version { get; set; }
    /// <summary>
    /// A list of HTML tags that the client allows / supports in Markdown.
    /// </summary>
    public string[]? allowedTags { get; set; }
}
