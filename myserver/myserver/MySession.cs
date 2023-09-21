using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Nodes;
using MyServer.Misc;
using MyServer.Protocol;
using MyServer;

namespace myserver
{
    public class MySession
    {
        public static MySession Instance = new MySession();
        public static void RunSession(Stream inputStream, Stream outputStream)
        {
            try
            {
                MySession.Instance.Start(inputStream, outputStream).Wait();
            }
            catch { }

        }
        public static void RunServer(int port)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            serverSocket.Start();

            new System.Threading.Thread(() => {
                while (true)
                {
                    var clientSocket = serverSocket.AcceptSocket();
                    if (clientSocket != null)
                    {
                        My.Logger.Error(">> accepted connection from client");

                        new System.Threading.Thread(() => {
                            using (var networkStream = new NetworkStream(clientSocket))
                            {
                                try
                                {
                                    RunSession(networkStream, networkStream);
                                }
                                catch (Exception e)
                                {
                                    My.Logger.Error(e);
                                }
                            }
                            clientSocket.Close();
                            My.Logger.Error(">> client connection closed");
                            MyServerMgr.Instance.ExitApp();
                        }).Start();
                    }
                }
            }).Start();
        }

        protected const int BUFFER_SIZE = 4096;
        protected const string TWO_CRLF = "\r\n\r\n";
        protected const string CONTENT_LENGTH_HEAD = "Content-Length: ";
        protected static readonly Regex CONTENT_LENGTH_MATCHER = new Regex(@"Content-Length: (\d+)");

        protected static readonly Encoding Encoding = System.Text.Encoding.UTF8;

        private Stream _outputStream;

        private ByteBuffer _rawData = new ByteBuffer();
        private int _bodyLength = -1;

        private bool _stopRequested;

        public async Task Start(Stream inputStream, Stream outputStream)
        {
            _outputStream = outputStream;

            byte[] buffer = new byte[BUFFER_SIZE];

            _stopRequested = false;
            while (!_stopRequested)
            {
                var read = await inputStream.ReadAsync(buffer, 0, buffer.Length);

                if (read == 0)
                {
                    // end of stream
                    break;
                }

                if (read > 0)
                {
                    _rawData.Append(buffer, read);
                    ProcessData();
                }
            }
        }

        public void SendData(JsonNode data)
        {
            if(_outputStream is not null)
            {
                var str = data.ToJsonString();
                var bytes = Encoding.GetBytes(str);
                var len = bytes.Length.ToString();
                _outputStream.Write(Encoding.GetBytes(CONTENT_LENGTH_HEAD));
                _outputStream.Write(Encoding.GetBytes(len));
                _outputStream.Write(Encoding.GetBytes(TWO_CRLF));
                _outputStream.Write(bytes);
            }
        }

        public void Stop()
        {
            _stopRequested = true;
        }

        private void ProcessData()
        {
            while (true)
            {
                if (_bodyLength >= 0)
                {
                    if (_rawData.Length >= _bodyLength)
                    {
                        var buf = _rawData.RemoveFirst(_bodyLength);

                        _bodyLength = -1;

                        var content = Encoding.GetString(buf);
                        var json = JsonNode.Parse(content)!;

                        JsonRpcMgr.Instance.ReceiveData(json);

                        continue;   // there may be more complete messages to process
                    }
                }
                else
                {
                    string s = _rawData.GetString(Encoding);
                    var idx = s.IndexOf(TWO_CRLF);
                    if (idx != -1)
                    {
                        Match m = CONTENT_LENGTH_MATCHER.Match(s);
                        if (m.Success && m.Groups.Count == 2)
                        {
                            _bodyLength = Convert.ToInt32(m.Groups[1].ToString());

                            _rawData.RemoveFirst(idx + TWO_CRLF.Length);

                            continue;   // try to handle a complete message
                        }
                    }
                }
                break;
            }
        }

    }

    class ByteBuffer
    {
        private byte[] _buffer;

        public ByteBuffer()
        {
            _buffer = new byte[0];
        }

        public int Length
        {
            get { return _buffer.Length; }
        }

        public string GetString(Encoding enc)
        {
            return enc.GetString(_buffer);
        }

        public void Append(byte[] b, int length)
        {
            byte[] newBuffer = new byte[_buffer.Length + length];
            System.Buffer.BlockCopy(_buffer, 0, newBuffer, 0, _buffer.Length);
            System.Buffer.BlockCopy(b, 0, newBuffer, _buffer.Length, length);
            _buffer = newBuffer;
        }

        public byte[] RemoveFirst(int n)
        {
            byte[] b = new byte[n];
            System.Buffer.BlockCopy(_buffer, 0, b, 0, n);
            byte[] newBuffer = new byte[_buffer.Length - n];
            System.Buffer.BlockCopy(_buffer, n, newBuffer, 0, _buffer.Length - n);
            _buffer = newBuffer;
            return b;
        }
    }
}
