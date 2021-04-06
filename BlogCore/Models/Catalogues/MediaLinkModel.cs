using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Catalogues
{
    public class MediaLinkModel
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public MediaGroupModel Group { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
