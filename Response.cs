using Newtonsoft.Json;

namespace ZabbixSenderCore
{
    public class Response
    {

        [JsonProperty("response")]
        public string Status { get; private set; }

        [JsonProperty("info")]
        public string InfoString { get; private set; }

        public Response(string response, string info)
        {
            this.Status = response;
            this.InfoString = info;
        }
    }
}