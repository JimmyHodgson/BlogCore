using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.Catalogues
{
    public class AchievementModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Type { get; set; }
        public short Year { get; set; }
    }
}
