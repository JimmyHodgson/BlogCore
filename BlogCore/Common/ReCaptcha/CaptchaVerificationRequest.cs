using Newtonsoft.Json;

namespace BlogCore.Common.ReCaptcha
{
    public class CaptchaVerificationRequest
    {
        [JsonProperty("event")]
        public GoogleEvent Event { get; set; }
    }
}
