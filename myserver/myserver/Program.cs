// See https://aka.ms/new-console-template for more information
using myserver;
using MyServer.Misc;
using System.Text.Json;
using System.Text.Json.Nodes;
using Protocol = MyServer.Protocol;

My.Logger.Info("Hello World");
My.Logger.Info($"pwd={Environment.CurrentDirectory}");

MySession.RunServer(40080);

if (false){
    var jsonstr = File.ReadAllText("tmp/initialize.jsonc");
    JsonDocument.Parse(jsonstr);
    var msg = JsonNode.Parse(jsonstr);
    var init_json = msg!["params"];
    var init_str = init_json!.ToJsonString();
    var init_params = init_json.Deserialize< Protocol.InitializeParams >();
    //var init_params = JsonSerializer.Deserialize<Protocol.InitializeParams>(init_str);
    var serializeOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    var str = JsonSerializer.Serialize(init_params, serializeOptions);
    Console.WriteLine(str);
}