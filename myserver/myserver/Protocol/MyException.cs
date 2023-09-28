using NLog.Layouts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyServer.Protocol
{
    /// <summary>
    /// Well-known LSP error codes.
    /// </summary>
    public static class ErrorCodes
    {
        #region JsonRpc Reserved Error Range 
        public const int JsonrpcReservedErrorRangeEnd = -32000;
        /// <summary>
        /// No error code was supplied.
        /// </summary>
        public const int UnknownErrorCode = -32001;

        /// <summary>
        /// Error code indicating that a server received a notification or
        /// request before the server has received the `initialize` request.
        /// </summary>
        public const int ServerNotInitialized = -32002;

        /// <summary>
        /// This is the start range of JSON-RPC reserved error codes.
        /// It doesn't denote a real error code. No LSP error codes should
        /// be defined between the start and end range.For backwards
        /// compatibility the `ServerNotInitialized` and the `UnknownErrorCode`
        /// are left in the range.
        /// </summary>
        public const int JsonrpcReservedErrorRangeStart = -32099;
        #endregion  JsonRpc Reserved Error Range

        #region Defined by JSON-RPC

        /// <summary>
        /// Invalid request.
        /// </summary>
        public const int InvalidRequest = -32600;

        /// <summary>
        /// Method not found.
        /// </summary>
        public const int MethodNotSupported = -32601;

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

        #endregion Defined by JSON-RPC

        public const int LspReservedErrorRangeEnd = -32800;

        /// <summary>
        /// Request was cancelled.
        /// </summary>
        public const int RequestCancelled = -32800;

        /// <summary>
        /// The server detected that the content of a document got
        /// modified outside normal conditions.A server should
        /// NOT send this error code if it detects a content change
        /// in it unprocessed messages.The result even computed
        /// on an older state might still be useful for the client.
        /// 
        /// If a client decides that a result is not of any use anymore
        /// the client should cancel the request.
        /// </summary>
        public const int ContentModified = -32801;

        /// <summary>
        /// The server cancelled the request.This error code should
        /// only be used for requests that explicitly support being
        /// server cancellable.
        /// </summary>
        public const int ServerCancelled = -32802;

        /// <summary>
        /// A request failed but it was syntactically correct, e.g the
        /// method name was known and the parameters were valid.The error
        /// message should contain human readable information about why
        /// the request failed.
        /// </summary>
        public const int RequestFailed = -32803;

        public const int LspReservedErrorRangeStart = -32899;

    }

    public class MyException:Exception
    {
        private int _code;
        private string _message;
        public MyException(int code, string message) { 
            _code = code;
            _message = message;
        }

        public override sealed string Message => _message;
        public int Code => _code;

        public virtual ResponseError ToResponseError()
        {
            return new ResponseError
            {
                code = _code,
                message = _message,
            };
        }
    }
}
