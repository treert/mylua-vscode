using MyServer.JsonRpc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    // lsp 里定义的一些 id: integer | string;
    public partial record MyId : IEquatable<int>, IEquatable<string>
    {
        private int? _number;
        private string? _string;

        public MyId(int value)
        {
            _number = value;
            _string = null;
        }

        public MyId(string value)
        {
            _number = null;
            _string = value;
        }

        public bool IsNumber => _number.HasValue;

        public int Number
        {
            get => _number ?? 0;
            set
            {
                _string = null;
                _number = value;
            }
        }

        public bool IsString => _string != null;

        public string String
        {
            get => _string ?? string.Empty;
            set
            {
                _string = value;
                _number = null;
            }
        }

        public static implicit operator MyId(int value) => new MyId(value);

        public static implicit operator MyId(string value) => new MyId(value);

        public bool Equals(int other) => IsNumber && _number == other;
        public bool Equals(string? other) => IsString && _string == other;

        private string DebuggerDisplay => IsString ? String : IsNumber ? Number.ToString() : "";

        public override string ToString() => DebuggerDisplay;
    }

    public class ResponseError
    {
        public int code;
        public string message;
        public JsonNode? data;
    }

    public class CancelParams{
        public int id;
    }

    public record ProgressToken : MyId
    {
        public ProgressToken(int value) : base(value)
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

    public class InitArgs : IJson
    {
        public int? processId;
        public (string name,string? version)? clientInfo;
        public string? locale;
        public JsonNode? initializationOptions;

        public void ReadFrom(JsonNode? node)
        {
            Debug.Assert(node != null);
            processId = node["processId"]?.GetValue<int>();
            var clientInfo_node = node["clientInfo"];
            if (clientInfo_node != null)
            {
                var name = clientInfo_node["name"]!.GetValue<string>();
                var version = clientInfo_node["version"]?.GetValue<string>();
                clientInfo = (name,version);
            }
            locale = node["locale"]?.GetValue<string>();
        }

        public JsonNode? ToJsonNode()
        {
            throw new NotImplementedException();
        }
    }

    public class InitResult : IJson
    {
        public (string name, string? version)? serverInfo;

        public void ReadFrom(JsonNode? node)
        {
            throw new NotImplementedException();
        }

        public JsonNode? ToJsonNode()
        {
            throw new NotImplementedException();
        }
    }

    //public class  InitRpc:JsonRpcBase<InitArgs, InitResult>
    //{
        
    //}
}
