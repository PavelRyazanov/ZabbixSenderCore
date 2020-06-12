using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZabbixSenderCore
{
    public class ZabbixRequest
    {
        [JsonProperty("request")]
        public string TypeAgent => "sender data";

        [JsonProperty("data")]
        public ZabbixData[] Content { get; private set; }

        [JsonProperty("clock", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? Clock { get; set; } = DateTime.Now;

        public ZabbixRequest(ZabbixData[] data)
        {
            this.Content = data;
        }
    }
}