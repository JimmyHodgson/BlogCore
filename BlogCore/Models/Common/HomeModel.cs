using System;

namespace BlogCore.Models.Common
{
    public class HomeModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LandingImage { get; set; }
        public string SkillImage { get; set; }
        public string ContactImage { get; set; }
        public string GithubLink { get; set; }
        public string LinkedInLink { get; set; }
        public string CVLink { get; set; }
    }
}
