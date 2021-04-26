using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.Catalogues
{
    public class SkillModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Proficiency { get; set; }
    }
}
