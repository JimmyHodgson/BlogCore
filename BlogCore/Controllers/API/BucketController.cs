using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogCore.Controllers.API
{
    [Route("api/[controller]")]
    public class BucketController : Controller
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;
        public BucketController(IAmazonS3 s3Client, IConfiguration config)
        {
            _s3Client = s3Client;
            _config = config;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<S3Bucket>>> Get()
        {
            var response = await _s3Client.ListBucketsAsync();
            return response.Buckets.FindAll(x => x.BucketName.Contains("blogcore."));
        }

        // GET api/<controller>/5
        [HttpGet("{name}")]
        public async Task<ActionResult<S3Bucket>> Get(string name)
        {
            var response = await _s3Client.ListBucketsAsync();
            var bucket = response.Buckets.Find(x => x.BucketName == name);
            if (bucket==null)
            {
                return NotFound();
            }
            return bucket;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<PutBucketResponse> Post([FromBody]string name)
        {
            name = $"blogcore.{name}";
            PutBucketRequest request = new PutBucketRequest
            {
                BucketName = name,
            };

            PutBucketResponse response = await _s3Client.PutBucketAsync(request);

            return response;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
