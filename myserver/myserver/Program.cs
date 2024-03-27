// See https://aka.ms/new-console-template for more information
using myserver;
using MyServer.Compiler;
using MyServer.Misc;
using MyServer.Protocol;
using System.Text.Json;
using System.Text.Json.Nodes;
using Protocol = MyServer.Protocol;

My.Logger.Info("Hello World");
My.Logger.Info($"pwd={Environment.CurrentDirectory}");

LuaFile file = new LuaFile("123213");

//JsonRpcMgr.Instance.Init();
//MySession.RunServer(40080);
//My.Logger.Info("Server Start");

