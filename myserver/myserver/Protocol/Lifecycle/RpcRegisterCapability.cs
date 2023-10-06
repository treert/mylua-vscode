
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
    public JsonNode? registerOptions { get; set; }
}

public class RegistrationParams
{
    public List<Registration> registrations { get; set; }
}

public class RpcRegisterCapability : JsonRpcBase<RegistrationParams, Dummy>
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

public class UnRegistrationParams
{
    public List<UnRegistration> unregisterations { get; set; }
}

public class RpcUnregisterCapability : JsonRpcBase<UnRegistrationParams, Dummy>
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

