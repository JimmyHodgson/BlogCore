using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Common.ReCaptcha
{
    public class CaptchaVerificationService
    {
        private CaptchaSettings _captchaSettings;
        public CaptchaVerificationService(IOptions<CaptchaSettings> captchaSettings)
        {
            _captchaSettings = captchaSettings.Value;
        }

        public async Task<bool> IsCaptchaValid(string token)
        {
            bool result = false;

            var request = new CaptchaVerificationRequest();
            var gEvent = new GoogleEvent
            {
                ExpectedAction = Constants.CaptchaUserActions.WebsiteMessage,
                SiteKey = _captchaSettings.SiteKey,
                Token = token
            };

            request.Event = gEvent;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(request,settings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"{_captchaSettings.GoogleVerificationUrl}/{_captchaSettings.ProjectId}/assessments?key={_captchaSettings.APIKey}",data);
                string jsonString = await response.Content.ReadAsStringAsync();
                CaptchaVerificationResponse captchaVerfication = JsonConvert.DeserializeObject<CaptchaVerificationResponse>(jsonString);
                if(captchaVerfication.Score > 0.7)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
