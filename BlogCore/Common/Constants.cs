namespace BlogCore.Common
{
    public static class Constants
    {
        private const short BCMajor = 2;
        private const short BCMinor = 0;
        private const short BCPatch = 0;

        public static readonly string BCVersion = $"{BCMajor}.{BCMinor}.{BCPatch}";

        public static readonly string BlogCoreConfigPrefix = "BC_";

        public static readonly string ThumbnailGroup = "__thumbnails";
        public struct Configuration
        {
            public const string DatabaseName = "database:dbName";
            public const string DatabaseSecret = "database:secret";
            public const string DatabaseServer = "database:server";
            public const string DatabaseUser = "database:user";
            public const string MailServer = "mail:server";
            public const string MailPort = "mail:port";
            public const string MailSenderAccount = "mail:senderaccount";
            public const string MailSenderName = "mail:sendername";
            public const string MailAccount = "mail:account";
            public const string MailSecret = "mail:secret";
            public const string MailTLS = "mail:tls";
        }

        public struct EmailValues
        {
            public const string Name = "{{name}}";
            public const string ReplyEmail = "{{replyemail}}";
            public const string Message = "{{message}}";
            public const string HelpEmail = "{{helpemail}}";
            public const string ConfirmationLink = "{{confirmationlink}}";
            public const string PasswordResetLink = "{{passwordresetlink}}";
        }

        public struct EmailTemplates
        {
            public const string WelcomeTemplate = "Welcome.html";
            public const string ConfirmationLinkTemplate = "Confirmation.html";
            public const string PasswordResetTemplate = "PasswordReset.html";
            public const string WebsiteMessageTemplate = "WebsiteMessage.html";
        }

        public struct CaptchaUserActions
        {
            public const string WebsiteMessage = "Website_Message";
        }
    }
}
