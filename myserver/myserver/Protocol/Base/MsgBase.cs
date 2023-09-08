using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    public class MsgBase : IJson
    {
        public string jsonrpc;

        public virtual void ReadFrom(JsonNode node)
        {
            jsonrpc = node["jsonrpc"]!.GetValue<string>();
        }

        public JsonNode ToJsonNode()
        {
            return jsonrpc!;
        }
    }

    // lsp 里定义的一些 id: integer | string;
    public partial record MyId : IEquatable<long>, IEquatable<string>
    {
        private long? _long;
        private string? _string;

        public MyId(long value)
        {
            _long = value;
            _string = null;
        }

        public MyId(string value)
        {
            _long = null;
            _string = value;
        }

        public bool IsLong => _long.HasValue;

        public long Long
        {
            get => _long ?? 0;
            set
            {
                _string = null;
                _long = value;
            }
        }

        public bool IsString => _string != null;

        public string String
        {
            get => _string ?? string.Empty;
            set
            {
                _string = value;
                _long = null;
            }
        }

        public static implicit operator MyId(long value) => new MyId(value);

        public static implicit operator MyId(string value) => new MyId(value);

        public bool Equals(long other) => IsLong && _long == other;
        public bool Equals(string? other) => IsString && _string == other;

        private string DebuggerDisplay => IsString ? String : IsLong ? Long.ToString() : "";

        public override string ToString() => DebuggerDisplay;
    }

    public class RequestMsg : MsgBase
    {
        public MyId id;
        public string method;
        public IJson? args;

        public static void F()
        {
            RequestMsg msg = new RequestMsg();
            msg.id = 12;
            Dictionary<MyId, string> a = new Dictionary<MyId, string>()
            {
                {1,"2341" },
            };
        }
    }

    public abstract class MyRpc<T1,T2> where T1 : IJson where T2 : IJson
    {
        public T1 request;
        public T2? response;

        public abstract Task Process();
    }

    public class InitRpc : MyRpc<ResponseMsg, ResponseMsg>
    {
        public override async Task Process()
        {
            await Task.Delay(500);
        }
    }

    public class ResponseMsg : MsgBase
    {
        public int id;
        public IJson? result;
        public IJson? error;
    }

    public class ResponseError
    {
        public int code;
        public string message;
        public IJson? data;
    }

    public class NotifyMsg : MsgBase
    {
        public string message;
        public IJson? data;
    }

    public class CancelParams{
        public int id;
    }


    public record ProgressToken : MyId
    {
        public ProgressToken(long value) : base(value)
        {
        }

        public ProgressToken(string value) : base(value)
        {
        }

        protected ProgressToken(MyId original) : base(original)
        {
        }
    }

    /// <summary>
    /// Well-known LSP error codes.
    /// </summary>
    public static class ErrorCodes
    {
        /// <summary>
        /// No error code was supplied.
        /// </summary>
        public const int UnknownErrorCode = -32001;

        /// <summary>
        /// An exception was thrown by a .net server / client
        /// </summary>
        public const int Exception = -32050;

        /// <summary>
        /// Server has not been initialised.
        /// </summary>
        public const int ServerNotInitialized = -32002;

        /// <summary>
        /// Method not found.
        /// </summary>
        public const int MethodNotSupported = -32601;

        /// <summary>
        /// Invalid request.
        /// </summary>
        public const int InvalidRequest = -32600;

        /// <summary>
        /// Invalid request parameters.
        /// </summary>
        public const int InvalidParameters = -32602;

        /// <summary>
        /// Internal error.
        /// </summary>
        public const int InternalError = -32603;

        /// <summary>
        /// Unable to parse request.
        /// </summary>
        public const int ParseError = -32700;

        /// <summary>
        /// Request was cancelled.
        /// </summary>
        public const int RequestCancelled = -32800;

        /// <summary>
        /// Request was cancelled.
        /// </summary>
        public const int ContentModified = -32801;
    }

    public class WorkspaceFolder
    {
        public Uri uri;
        public string name;
    }

    public class InitArgs
    {
        public int? processId;
        public (string name,string? version)? xx;
        public string? locale;
        public JsonNode? initializationOptions;
    }
}
