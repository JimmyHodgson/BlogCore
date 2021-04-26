using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.Catalogues
{
    public class EducationModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string School { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public short Year { get; set; }
    }
}
