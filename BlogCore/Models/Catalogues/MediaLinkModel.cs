using System;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.Catalogues
{
    public class MediaLinkModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public MediaGroupModel Group { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
