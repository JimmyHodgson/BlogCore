using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Common
{
    public static class TemplateReader
    {
        public static string GetTemplate(string template,List<KeyValuePair<string,string>> variables)
        {
            string message = "";
            string templatePath = Path.Combine("templates", template);
            using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
            {
                message = SourceReader.ReadToEnd();
            }

            foreach(KeyValuePair<string,string> variable in variables)
            {
                message = message.Replace(variable.Key, variable.Value);
            }
            return message;
        }
    }
}
