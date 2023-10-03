﻿using MyServer.JsonRpc;
using MyServer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
似乎可以不支持的

服务器动态注册能力。
1. 不是所有客户端都支持的。
2. 一个能力注册方式二选一：通过 RpcInit or 通过动态注册。
 */

namespace MyServer.Protocol;

public class Registration
{
    public string id { get; set; }
    public string method { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonNode? registerOptions { get; set; }
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
        obj["registrations"] = regs.ToJsonNode();
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

public class UnRegistration
{
    public string id { get; set; }
    public string method { get; set; }
}

public class UnRegistrationParams : IJson
{
    public List<UnRegistration> regs = new();
    public void ReadFrom(JsonNode node)
    {
        regs = node["unregisterations"]!.ConvertTo<List<UnRegistration>>();
    }

    public JsonNode ToJsonNode()
    {
        JsonObject obj = new JsonObject();
        obj["unregisterations"] = regs.ToJsonNode();
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

