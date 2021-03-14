using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Common
{
    public static class Constants
    {
        private const short BCMajor = 1;
        private const short BCMinor = 0;
        private const short BCPatch = 0;

        public static readonly string BCVersion = $"{BCMajor}.{BCMinor}.{BCPatch}";
        public struct Env
        {
            public const string SqlSecret = "JHMSSQLSECRET";
            public const string MailServer = "JHMAILSERVER";
            public const string MailPort = "JHMAILPORT";
            public const string MailSenderAccount = "JHMAILSENDERACCOUNT";
            public const string MailSenderName = "JHMAILSENDERNAME";
            public const string MailAccount = "JHMAILACCOUNT";
            public const string MailSecret = "JHMAILSECRET";
            public const string MailTLS = "JHMAILTLS";
        }

        public struct EmailValues
        {
            public const string Name = "{{name}}";
            public const string HelpEmail = "{{helpemail}}";
            public const string ConfirmationLink = "{{confirmationlink}}";
        }

        public struct EmailTemplates
        {
            public const string WelcomeTemplate = "Welcome.html";
            public const string ConfirmationLinkTemplate = "Confirmation.html";
        }
    }
}
