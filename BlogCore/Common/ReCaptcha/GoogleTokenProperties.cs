using Newtonsoft.Json;

namespace BlogCore.Common.ReCaptcha
{
    public class GoogleTokenProperties
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("createTime")]
        public string CreateTime { get; set; }
        [JsonProperty("hostname")]
        public string HostName { get; set; }
        [JsonProperty("invalidReason")]
        public string InvalidReason { get; set; }
        [JsonProperty("valid")]
        public bool Valid { get; set; }
    }
}
