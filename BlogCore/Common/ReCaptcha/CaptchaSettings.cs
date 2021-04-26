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
