using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Catalogues
{
    public class AchievementModel
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Title { get; set; }
        public string Type { get; set; }
        public short Year { get; set; }
    }
}
