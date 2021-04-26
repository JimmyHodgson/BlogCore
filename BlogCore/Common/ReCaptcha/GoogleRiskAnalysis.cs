using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlogCore.Common.ReCaptcha
{
    public class GoogleRiskAnalysis
    {
        [JsonProperty("reasons")]
        public List<string> Reasons { get; set; }
        [JsonProperty("score")]
        public float Score { get; set; }
    }
}
