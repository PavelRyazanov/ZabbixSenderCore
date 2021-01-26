using Newtonsoft.Json;

namespace ZabbixSenderCore
{
    public class ZabbixResponse
    {

        [JsonProperty("response")]
        public string Status { get; private set; }

        [JsonProperty("info")]
        public string InfoString { get; private set; }

        private ResponseInfo _responseInfo;

        public ResponseInfo ResponseInfo
        {
            get
            {
                if (_responseInfo is null)
                {
                    var parser = new ResponseParser();
                    _responseInfo = parser.Parse(this.InfoString);
                }

                return _responseInfo;
            }
        }

        public ZabbixResponse(string response, string info)
        {
            this.Status = response;
            this.InfoString = info;
        }
    }
}