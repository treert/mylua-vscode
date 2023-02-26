using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyServer.Protocol
{
    /// <summary>
    /// Uniform Resource Identifier (URI) http://tools.ietf.org/html/rfc3986.
    /// This class is a simple parser which creates the basic component parts
    /// (http://tools.ietf.org/html/rfc3986#section-3) with minimal validation
    /// and encoding.
    ///
    /// ```txt
    /// foo://example.com:8042/over/there?name=ferret#nose
    /// \_/   \______________/\_________/ \_________/ \__/
    /// |           |            |            |        |
    /// scheme     authority       path        query   fragment
    /// |   _____________________|__
    /// / \ /                        \
    /// urn:example:animal:ferret:nose
    /// ```
    /// </summary>
    /// <summary>
    /// This class describes a document uri as defined by https://microsoft.github.io/language-server-protocol/specifications/specification-current/#uri
    /// </summary>
    [JsonConverter(typeof(DocumentUriJsonConverter))]
    internal class DocumentUri:IEquatable<DocumentUri>
    {
        public bool Equals(DocumentUri? other)
        {
            return m_uri.Equals(other?.m_uri);
        }

        public static DocumentUri? Parse(string uri)
        {
            if(Uri.TryCreate(uri,UriKind.Absolute,out var t))
            {
                DocumentUri ret = new DocumentUri();
                ret.m_uri = t;
                return ret;
            }
            return null;
        }

        public override string ToString()
        {
            return m_uri.ToString();
        }

        private Uri m_uri;
    }

    internal class DocumentUriJsonConverter : JsonConverter<DocumentUri>
    {
        public override DocumentUri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DocumentUri.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DocumentUri value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
