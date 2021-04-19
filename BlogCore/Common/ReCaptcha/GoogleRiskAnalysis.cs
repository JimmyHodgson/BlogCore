using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
