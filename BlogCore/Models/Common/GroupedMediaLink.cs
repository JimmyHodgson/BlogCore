using BlogCore.Models.Catalogues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Common
{
    public class GroupedMediaLink
    {
        MediaGroupModel Group { get; set; }
        IEnumerable<MediaLinkModel> MediaLinks { get; set; }
    }
}
