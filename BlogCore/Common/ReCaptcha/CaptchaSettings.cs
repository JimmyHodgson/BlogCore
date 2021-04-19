using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Common.ReCaptcha
{
    public class CaptchaSettings
    {
        public string APIKey { get; set; }
        public string SiteKey { get; set; }
        public string ProjectId { get; set; }
        public string GoogleVerificationUrl { get; set; }
    }
}
