using Newtonsoft.Json;

namespace ZabbixSenderCore
{
    public class ZabbixResponse
    {

        [JsonProperty("response")]
        public string Status { get; private set; }

        [JsonProperty("info")]
        public string InfoString { get; private set; }

        public ZabbixResponse(string response, string info)
        {
            this.Status = response;
            this.InfoString = info;
        }
    }
}