// See https://aka.ms/new-console-template for more information
using myserver;
using System.Text.Json;
using System.Text.Json.Nodes;
using Protocol = MyServer.Protocol;

Console.WriteLine("Hello, World!");
Console.WriteLine($"pwd={Environment.CurrentDirectory}");

//MySession.RunServer(40080);

if(true){
    var jsonstr = File.ReadAllText("tmp/initialize.jsonc");
    var msg = JsonNode.Parse(jsonstr);
    var init_json = msg!["params"];
    var init_str = init_json!.ToJsonString();
    var init_params = JsonSerializer.Deserialize<Protocol.InitializeParams>(init_str);
    var serializeOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    var str = JsonSerializer.Serialize(init_params, serializeOptions);
    Console.WriteLine(str);
}