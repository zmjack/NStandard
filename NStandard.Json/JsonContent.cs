using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace NStandard.Json
{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) : base(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json") { }
        public JsonContent(object obj, JsonSerializerOptions options) : base(JsonSerializer.Serialize(obj, options), Encoding.UTF8, "application/json") { }
        public JsonContent(object obj, JsonSerializerOptions options, Encoding encoding) : base(JsonSerializer.Serialize(obj, options), encoding, "application/json") { }
    }
}
