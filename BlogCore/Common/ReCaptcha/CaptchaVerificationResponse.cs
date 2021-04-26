using Newtonsoft.Json;

namespace BlogCore.Common.ReCaptcha
{
    public class CaptchaVerificationResponse
    {
        [JsonProperty("event")]
        public GoogleEvent Event { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("riskAnalysis")]
        public GoogleRiskAnalysis RiskAnalysis { get; set; }
        [JsonProperty("score")]
        public float Score { get; set; }
        [JsonProperty("tokenProperties")]
        public GoogleTokenProperties TokenProperties { get; set; }
    }
}
