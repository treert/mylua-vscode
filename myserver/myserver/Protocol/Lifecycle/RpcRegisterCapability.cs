using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

/*
似乎可以不支持的

服务器动态注册能力。
1. 不是所有客户端都支持的。
2. 一个能力注册方式二选一：通过 RpcInit or 通过动态注册。
 */

namespace MyServer.Protocol;

public class Registration : IJson
{
    public string id;
    public string method;
    public JsonNode? registerOptions;

    public void ReadFrom(JsonNode node)
    {
        throw new NotImplementedException();
    }

    public JsonNode ToJsonNode()
    {
        JsonObject obj = new JsonObject()
        {
            { "id", id },
            { "method", method },
            { "registerOptions", registerOptions }
        };
        return obj;
    }
}

public class RegistrationParams : IJson
{
    public List<Registration> regs = new();
    public void ReadFrom(JsonNode node)
    {
        throw new NotImplementedException();
    }

    public JsonNode ToJsonNode()
    {
        JsonObject obj = new JsonObject();
        JsonArray arr = new JsonArray();
        foreach (Registration registration in regs)
        {
            arr.Add(registration.ToJsonNode());
        }
        obj["registrations"] = arr;
        return obj;
    }
}

public class RpcRegisterCapability : JsonRpcBase<RegistrationParams, EmptyObject>
{
    public override string m_method => "client/registerCapability";

    public override void OnCanceled()
    {
        throw new NotImplementedException();
    }

    public override void OnRequest()
    {
        throw new NotImplementedException();
    }

    protected override void OnSuccess()
    {
        // todo 
    }
}

public class UnRegistration : IJson
{
    public string id;
    public string method;

    public void ReadFrom(JsonNode node)
    {
        id = node["id"]!.GetValue<string>();
        method = node["method"]!.GetValue<string>();
    }

    public JsonNode ToJsonNode()
    {
        JsonObject obj = new JsonObject()
        {
            { "id", id },
            { "method", method },
        };
        return obj;
    }
}

public class UnRegistrationParams : IJson
{
    public List<UnRegistration> regs = new();
    public void ReadFrom(JsonNode node)
    {
        regs.Clear();
        JsonArray arr = node["unregisterations"]!.AsArray();
        foreach (JsonNode? it in arr)
        {
            var reg = new UnRegistration();
            reg.ReadFrom(it!);
            regs.Add(reg);
        }
    }

    public JsonNode ToJsonNode()
    {
        JsonObject obj = new JsonObject();
        JsonArray arr = new JsonArray();
        foreach (UnRegistration registration in regs)
        {
            arr.Add(registration.ToJsonNode());
        }
        obj["unregisterations"] = arr;
        return obj;
    }
}

public class RpcUnregisterCapability : JsonRpcBase<UnRegistrationParams, EmptyObject>
{
    public override string m_method => "client/unregisterCapability";

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

