using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogCore.Models;
using NETCore.MailKit.Core;
using Microsoft.AspNetCore.Hosting;
using BlogCore.Common;

namespace BlogCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;
        
        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            List<KeyValuePair<string, string>> variables = new List<KeyValuePair<string, string>>();
            variables.Add(new KeyValuePair<string, string>(Constants.EmailValues.Name, "Jimmy Hodgson"));
            variables.Add(new KeyValuePair<string, string>(Constants.EmailValues.HelpEmail, "Jimmy.hodgson@outlook.com"));
            string message = TemplateReader.GetTemplate(Constants.EmailTemplates.WelcomeTemplate,variables);
            ViewData["template"] = message;

            //_emailService.Send("jimmy.hodgson@outlook.com", "Welcome!", message, true);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
