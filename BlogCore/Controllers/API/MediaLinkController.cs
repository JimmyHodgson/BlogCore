using Amazon.S3;
using Amazon.S3.Model;
using BlogCore.Common;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using BlogCore.Models.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize(Roles = "Administrator")]
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

        [HttpDelete]
        [ODataRoute("({Id})")]
        public async Task<ActionResult<MediaLinkModel>> Delete(Guid id)
        {
            MediaLinkModel model = await _context.MediaLinks.Include(x=>x.Group).FirstOrDefaultAsync(x => x.Id == id);
            if(model == null)
            {
                return NotFound($"Image with Id {id} not found.");
            }

            string modelKey = $"{model.Group.NormalizedName}/{model.Name}";
            string thumbnailKey = $"{Constants.ThumbnailGroup}/{modelKey}";

            DeleteObjectsRequest request = new DeleteObjectsRequest
            {
                BucketName = _config["aws:bucket"],
                Objects = new List<KeyVersion>() { new KeyVersion() { Key = modelKey }, new KeyVersion() { Key = thumbnailKey } }
            };

            try
            {
                await _s3Client.DeleteObjectsAsync(request);
                _context.MediaLinks.Remove(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<MediaLinkModel>> Post([FromForm]CreateMediaLinkViewModel model)
        {
            MediaLinkModel exists = await _context.MediaLinks.FirstOrDefaultAsync(x => x.Name == model.Name);
            MediaGroupModel group = await _context.MediaGroups.FirstOrDefaultAsync(x => x.Id == model.Group);

            if (exists != null)
            {
                return BadRequest($"An image with name {model.Name} already exists.");
            }

            using (MemoryStream thumbnail = CreateThumbnail(model.File))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                model.File.CopyTo(memoryStream);

                try
                {
                    await PutImageAsync(memoryStream, $"{group.NormalizedName}/{model.Name}");
                    await PutImageAsync(thumbnail, $"{Constants.ThumbnailGroup}/{group.NormalizedName}/{model.Name}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            MediaLinkModel image = new MediaLinkModel()
            {
                Name = model.Name,
                Group = group,
                Url = $"https://{_config["aws:bucket"]}.s3.amazonaws.com/{group.NormalizedName}/{model.Name}",
                Thumbnail = $"https://{_config["aws:bucket"]}.s3.amazonaws.com/{Constants.ThumbnailGroup}/{group.NormalizedName}/{model.Name}"
            };

            try
            {
                await _context.MediaLinks.AddAsync(image);
                await _context.SaveChangesAsync();
                return Ok(image);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private MemoryStream CreateThumbnail(IFormFile file)
        {
            MemoryStream outStream = new MemoryStream();
            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                using Image image = Image.Load(stream);
                image.Mutate(x => x.Resize(300, 0));
                image.Save(outStream, image.Metadata.DecodedImageFormat);
                outStream.Position = 0;
                return outStream;
            }
        }

        private async Task<PutObjectResponse> PutImageAsync(MemoryStream stream, string key)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                BucketName = _config["aws:bucket"],
                Key = key,
                CannedACL = S3CannedACL.PublicRead
            };

            return await _s3Client.PutObjectAsync(request);
        }
    }
}
