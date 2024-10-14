using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlogCore.Common.ReCaptcha
{
    public class CaptchaVerificationService
    {
        private readonly CaptchaSettings _captchaSettings;
        private readonly ILogger<CaptchaVerificationService> _logger;
        public CaptchaVerificationService(IOptions<CaptchaSettings> captchaSettings, ILogger<CaptchaVerificationService> logger)
        {
            _captchaSettings = captchaSettings.Value;
            _logger = logger;
        }

        public async Task<bool> IsCaptchaValid(string token)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"secret", _captchaSettings.APIKey },
                {"response", token }
            });

            using (HttpClient client = new())
            {
                HttpResponseMessage response = await client.PostAsync(_captchaSettings.GoogleVerificationUrl,content);

                string responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<reCaptchaResponse>();
                    return result.Success;
                }
            }

            return false;
        }
    }

    public class reCaptchaResponse
    {
        public bool Success { get; set; }
        public string[] ErrorCodes { get; set; }
    }
}
