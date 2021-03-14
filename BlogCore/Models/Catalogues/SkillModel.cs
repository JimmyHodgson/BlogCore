﻿using BlogCore.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Catalogues
{
    public class SkillModel
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public string Proficiency { get; set; }
        public List<LinkModel> Links { get; set; }
    }
}
