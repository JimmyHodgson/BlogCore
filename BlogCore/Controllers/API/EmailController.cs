using BlogCore.Common;
using BlogCore.Common.ReCaptcha;
using BlogCore.Models.Common;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly DatabaseContext _context;
        private readonly CaptchaVerificationService _verificationService;
        public EmailController(IEmailService emailService, DatabaseContext context, CaptchaVerificationService verificationService)
        {
            _emailService = emailService;
            _context = context;
            _verificationService = verificationService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EmailMessageViewModel model)
        {
            var user = _context.Users.FirstOrDefault();
            if(user == null) {
                return BadRequest("No user is registered.");
            }

            var requestIsValid = await _verificationService.IsCaptchaValid(model.Token);

            if (requestIsValid)
            {
                List<KeyValuePair<string, string>> variables = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(Constants.EmailValues.Name, model.Name),
                        new KeyValuePair<string, string>(Constants.EmailValues.ReplyEmail, model.Email),
                        new KeyValuePair<string, string>(Constants.EmailValues.Message, model.Message)
                    };
                var message = TemplateReader.GetTemplate(Constants.EmailTemplates.WebsiteMessageTemplate, variables);

                _emailService.Send(user.Email, "Blogcore Message", message, true);
                return Ok(model);
            }
            return BadRequest("Request did not pass validation");
            
        }
    }
}