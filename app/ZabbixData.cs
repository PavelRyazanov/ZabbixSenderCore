using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZabbixSenderCore
{
    public class ZabbixData
    {
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public object Value { get; private set; }

        [JsonProperty("clock", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? Clock { get; set; }

        protected ZabbixData(string host, string key, DateTime? clock = null)
        {
            this.Host = host;
            this.Key = key;
            this.Clock = clock;
        }

        public ZabbixData(string host, string key, string value, DateTime? clock = null)
            : this(host, key, clock)
        {
            this.Value = value;
        }

        public ZabbixData(string host, string key, int value, DateTime? clock = null)
            : this(host, key, clock)
        {
            this.Value = value;
        }

        public ZabbixData(string host, string key, float value, DateTime? clock = null)
            : this(host, key, clock)
        {
            this.Value = value;
        }
    }
}
