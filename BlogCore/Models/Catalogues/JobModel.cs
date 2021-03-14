using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Catalogues
{
    public class JobModel
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Company { get; set; }
        public string Title { get; set; }
        public DateTime JobStart { get; set; }
        public DateTime? JobEnd { get; set; }
        public string Description { get; set; }
    }
}
