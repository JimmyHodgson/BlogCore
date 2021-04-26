using System.Collections.Generic;

namespace BlogCore.Models.Common
{
    public class StorageInfoModel
    {
        public long MaxValue { get; set; }
        public List<GroupInfoModel> Groups { get; set; }
    }
}
