using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.Common
{
    public class StorageInfoModel
    {
        public long MaxValue { get; set; }
        public List<GroupInfoModel> Groups { get; set; }
    }
}
