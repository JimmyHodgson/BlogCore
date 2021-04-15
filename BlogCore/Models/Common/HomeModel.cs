using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Common
{
    public class HomeModel
    {
        public Guid Id { get; set; } = new Guid();
        public string LandingImage { get; set; }
        public string SkillImage { get; set; }
        public string ContactImage { get; set; }
        public string GithubLink { get; set; }
        public string LinkedInLink { get; set; }
    }
}
