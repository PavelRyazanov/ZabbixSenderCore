using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZabbixSenderCore
{
    public class Data
    {
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("clock", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? Clock { get; set; }

        public Data(string host, string key, string value, DateTime? clock = null)
        {
            this.Host = host;
            this.Key = key;
            this.Value = value;
            this.Clock = clock;
        }
    }
}