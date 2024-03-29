﻿using Amazon.S3;
using Amazon.S3.Model;
using BlogCore.Common;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize(Roles = "Administrator")]
    [ODataRoutePrefix("MediaGroup")]
    public class MediaGroupController : ODataController
    {
        public readonly DatabaseContext _context;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;
        public MediaGroupController(DatabaseContext context, IAmazonS3 s3Client, IConfiguration config)
        {
            _context = context;
            _s3Client = s3Client;
            _config = config;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<MediaGroupModel> Get()
        {
            return _context.MediaGroups;
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({Id})")]
        public SingleResult<MediaGroupModel> Get(Guid id)
        {
            return SingleResult.Create(_context.MediaGroups.Where(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MediaGroupModel model)
        {
            MediaGroupModel exist = await _context.MediaGroups.FirstOrDefaultAsync(x => x.NormalizedName == model.NormalizedName);

            if (model.NormalizedName == Constants.ThumbnailGroup)
            {
                return BadRequest($"Normalized group name {model.NormalizedName} is restricted.");
            }
            if (exist != null)
            {
                return BadRequest($"Media group '{model.Name}' already exists.");
            }

            await _context.MediaGroups.AddAsync(model);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [ODataRoute("({Id})")]
        public async Task<ActionResult> Delete(Guid id)
        {
            MediaGroupModel model = await _context.MediaGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound($"Media Group with id '${id}' not found.");
            }

            List<KeyVersion> keys = new List<KeyVersion>();
            List<MediaLinkModel> images = await _context.MediaLinks.Include(x => x.Group).Where(x => x.Group.Id == model.Id).ToListAsync();

            foreach (MediaLinkModel image in images)
            {
                string imageKey = $"{model.NormalizedName}/{image.Name}";

                keys.Add(new KeyVersion() { Key = imageKey });
                keys.Add(new KeyVersion() { Key = $"{Constants.ThumbnailGroup}/{imageKey}" });
            }

            DeleteObjectsRequest request = new DeleteObjectsRequest
            {
                BucketName = _config["aws:bucket"],
                Objects = keys
            };

            try
            {
                if (keys.Count > 0)
                {
                    await _s3Client.DeleteObjectsAsync(request);
                }
                _context.MediaLinks.RemoveRange(images);
                _context.MediaGroups.Remove(model);
                await _context.SaveChangesAsync();
                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ODataRoute("({Id})")]
        public async Task<ActionResult> Put([FromODataUri]Guid id, [FromBody]MediaGroupModel model)
        {
            MediaGroupModel original = await _context.MediaGroups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (model.NormalizedName == Constants.ThumbnailGroup)
            {
                return BadRequest($"Normalized group name {model.NormalizedName} is restricted.");
            }

            if (original == null)
            {
                return NotFound($"Media Group with Name '{model.Name}' not found.");
            }

            List<MediaLinkModel> images = await _context.MediaLinks.Include(x => x.Group).Where(x => x.Group.Id == original.Id).ToListAsync();

            if (images.Count > 0)
            {
                return BadRequest($"Unable to edit a group with existing images.");
            }

            try
            {
                MediaGroupModel exists = await _context.MediaGroups.FirstOrDefaultAsync(x => x.NormalizedName == model.NormalizedName && x.Id != model.Id);
                if (exists != null)
                {
                    return BadRequest($"Media Group with Name {model.Name} already exists.");
                }

                _context.MediaGroups.Update(model);
                await _context.SaveChangesAsync();
                return Ok(model);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
