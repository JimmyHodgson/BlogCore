using Amazon.S3;
using Amazon.S3.Model;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using BlogCore.Models.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize]
    [ODataRoutePrefix("MediaLink")]
    public class MediaLinkController : ODataController
    {
        private readonly DatabaseContext _context;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;
        public MediaLinkController(DatabaseContext context, IAmazonS3 s3Client, IConfiguration config)
        {
            _context = context;
            _s3Client = s3Client;
            _config = config;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<MediaLinkModel> Get()
        {
            return _context.MediaLinks;
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public SingleResult<MediaLinkModel> Get(Guid id)
        {
            return SingleResult.Create(_context.MediaLinks.Where(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult<MediaLinkModel>> Post([FromForm]CreateMediaLinkViewModel model)
        {
            //TODO
            MediaLinkModel exists = await _context.MediaLinks.FirstOrDefaultAsync(x => x.Name == model.Name);
            MediaGroupModel group = await _context.MediaGroups.FirstOrDefaultAsync(x => x.Id == model.Group);

            if (exists != null)
            {
                return BadRequest($"An image with name {model.Name} already exists.");
            }


            using (MemoryStream memoryStream = new MemoryStream())
            {
                model.File.CopyTo(memoryStream);

                PutObjectRequest request = new PutObjectRequest
                {
                    InputStream = memoryStream,
                    BucketName = _config["aws:bucket"],
                    Key = $"{group.NormalizedName}/{model.Name}",
                    CannedACL = S3CannedACL.PublicRead
                };

                try
                {
                    await _s3Client.PutObjectAsync(request);

                    MediaLinkModel image = new MediaLinkModel()
                    {
                        Name = model.Name,
                        Group = group,
                        Url = $"https://{_config["aws:bucket"]}.s3.amazonaws.com/{group.NormalizedName}/{model.Name}"
                    };

                    await _context.MediaLinks.AddAsync(image);
                    await _context.SaveChangesAsync();

                    return Ok(image);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
