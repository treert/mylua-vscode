using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol.LanguageFeatues;

public class SelectionRangeParams : DocIdAndTokenParams
{
    public Position[] positions;
    public override void ReadFrom(JsonNode node)
    {
        base.ReadFrom(node);
        positions = node["positions"]!.ConvertTo<Position[]>();
    }

    public override JsonNode ToJsonNode()
    {
        var data = base.ToJsonNode().AsObject();
        data.AddKeyValue("positions", positions);
        return data;
    }
}

public class SelectionRange
{
    public Range range { get; set; }
    public SelectionRange? parent { get; set; }
}

public class SelectionRangeResult : IJson
{
    public List<SelectionRange> ranges = new();
    public void ReadFrom(JsonNode node)
    {
        ranges = 
    }

    public JsonNode ToJsonNode()
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// The selection range request is sent from the client to the server to return suggested selection ranges at an array of given positions.A selection range is a range around the cursor position which the user might be interested in selecting.
/// <br/>
/// A selection range in the return array is for the position in the provided parameters at the same index.Therefore positions[i] must be contained in result[i].range.To allow for results where some positions have selection ranges and others do not, result[i].range is allowed to be the empty range at positions[i].
/// <br/>
/// Typically, but not necessary, selection ranges correspond to the nodes of the syntax tree.
/// </summary>
public class RpcSelectionRange : JsonRpcBase<SelectionRangeParams, SelectionRangeResult>
{
    public override string m_method => "textDocument/selectionRange";

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
