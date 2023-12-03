using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol;

public class ConfigurationItem
{
    /// <summary>
    /// The scope to get the configuration section for.
    /// </summary>
    public string? scopeUri {  get; set; }
    /// <summary>
    /// The configuration section asked for.
    /// </summary>
    public string? section { get; set; }
}

public class ConfigurationParams
{
    public List<ConfigurationItem> items { get; set;}
}

[MyProto(Direction = ProtoDirection.ToClient)]
public class RpcGetConfiguration : JsonRpcBase<ConfigurationParams, List<JsonNode>>
{
    public override string m_method => "workspace/configuration";

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}
