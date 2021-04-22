using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class HomeViewModel
    {
        public User User { get; set; }
        public List<SkillModel> Skills { get; set; }
        public List<EducationModel> Education { get; set; }
        public List<AchievementModel> Achievements { get; set; }
        public List<JobModel> Jobs { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public string CaptchaClientKey { get; set; }
        public HomeModel Home { get; set; }

    }
}
