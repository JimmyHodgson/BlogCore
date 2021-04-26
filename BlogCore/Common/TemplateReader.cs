using System.Collections.Generic;
using System.IO;

namespace BlogCore.Common
{
    public static class TemplateReader
    {
        public static string GetTemplate(string template,List<KeyValuePair<string,string>> variables)
        {
            string message = "";
            string templatePath = Path.Combine("templates", template);
            using (StreamReader SourceReader = File.OpenText(templatePath))
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
