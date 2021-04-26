using Newtonsoft.Json;

namespace BlogCore.Common.ReCaptcha
{
    public class GoogleEvent
    {
        [JsonProperty("expectedAction")]
        public string ExpectedAction { get; set; }
        [JsonProperty("hashedAccountId")]
        public string HashedAccountId { get; set; }
        [JsonProperty("siteKey")]
        public string SiteKey { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }
        [JsonProperty("userIpAddress")]
        public string UserIpAddress { get; set; }
    }
}
