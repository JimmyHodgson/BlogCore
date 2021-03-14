using BlogCore.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Catalogues
{
    public class EducationModel
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string School { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public short Year { get; set; }
    }
}
