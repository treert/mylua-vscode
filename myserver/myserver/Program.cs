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

MyString myString = new MyString();
myString.ReplaceRange(0, 0, "0123456789");
myString.ReplaceRange(3, 6,"xyz");
myString.ReplaceRange(3, 6, "mn");
myString.ReplaceRange(3, 5, "abcde");


//JsonRpcMgr.Instance.Init();
//MySession.RunServer(40080);
//My.Logger.Info("Server Start");

My.Logger.Info("End");