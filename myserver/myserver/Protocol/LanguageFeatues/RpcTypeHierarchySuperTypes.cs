using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class TypeHierarchySupertypesParams : IJson, IWorkDoneProgress, IPartialResult
{
    public ProgressToken? partialResultToken { get; set; }
    public ProgressToken? workDoneToken { get; set; }

    public TypeHierarchyItem item;

    public virtual void ReadFrom(JsonNode node)
    {
        item = node["item"]!.ConvertTo<TypeHierarchyItem>();
        partialResultToken = node["partialResultToken"];
        workDoneToken = node["workDoneToken"];
    }

    public virtual JsonNode ToJsonNode()
    {
        JsonObject data = new JsonObject();
        data.AddKeyValue("item", item);
        data.TryAddKeyValue("partialResultToken", partialResultToken);
        data.TryAddKeyValue("workDoneToken", workDoneToken);
        return data;
    }
}

public class TypeHierarchySupertypesResult : IJson
{
    public List<TypeHierarchyItem> items = new List<TypeHierarchyItem>();
    public void ReadFrom(JsonNode node)
    {
        items = node.ConvertTo<List<TypeHierarchyItem>>();
    }

    public JsonNode ToJsonNode()
    {
        return items.ToJsonNode();
    }
}

public class RpcTypeHierarchySuperTypes : JsonRpcBase<TypeHierarchySupertypesParams, TypeHierarchySupertypesResult>
{
    public override string m_method => "typeHierarchy/supertypes";

    public override void OnCanceled()
    {
        // todo
    }

    public override void OnRequest()
    {
        // todo
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }
}

