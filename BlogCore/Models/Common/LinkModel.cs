using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Common
{
    public class LinkModel
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
