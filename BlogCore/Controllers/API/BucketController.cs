using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Common;
using BlogCore.Models.Common;
using BlogCore.Models.Catalogues;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogCore.Controllers.API
{
    [Route("api/[controller]")]
    public class BucketController : Controller
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;
        private readonly DatabaseContext _context;
        public BucketController(IAmazonS3 s3Client, IConfiguration config, DatabaseContext context)
        {
            _s3Client = s3Client;
            _config = config;
            _context = context;
        }

        [HttpGet]
        [Route("GetStorageInfo")]
        public async Task<ActionResult<StorageInfoModel>> GetStorageInfo()
        {
            List<string> prefixes = _context.MediaGroups.Select(x => $"{x.NormalizedName}/").ToList();
            prefixes.Add($"{Constants.ThumbnailGroup}/");
            List<GroupInfoModel> result = new List<GroupInfoModel>();
            string bucketMaxSize = _config["blogcore:bucketsize"];
            string bucket = _config["aws:bucket"];

            foreach (string prefix in prefixes)
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = bucket,
                    Prefix = prefix
                };

                var response = await _s3Client.ListObjectsV2Async(request);
                var size = response.S3Objects.Sum(x => x.Size);
                var count = response.S3Objects.Count();

                result.Add(new GroupInfoModel(){ Name = prefix, Size = size, Count = count });

            }
            return new StorageInfoModel() { MaxValue = StringToByteParser.Parse(bucketMaxSize), Groups = result };
        }
    }
}
