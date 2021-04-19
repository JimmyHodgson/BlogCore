using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Common.ReCaptcha
{
    public class CaptchaVerificationRequest
    {
        [JsonProperty("event")]
        public GoogleEvent Event { get; set; }
    }
}
