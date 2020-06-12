using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZabbixSenderCore
{
    public class Request
    {
        [JsonProperty("request")]
        public string TypeAgent => "sender data";

        [JsonProperty("data")]
        public Data[] Content { get; private set; }

        [JsonProperty("clock", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? Clock { get; set; } = DateTime.Now;

        public Request(Data[] data)
        {
            this.Content = data;
        }
    }
}