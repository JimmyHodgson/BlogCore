using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.Catalogues
{
    public class JobModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Company { get; set; }
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}")]
        public DateTime JobStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}")]
        public DateTime? JobEnd { get; set; }
        public string Description { get; set; }
    }
}
