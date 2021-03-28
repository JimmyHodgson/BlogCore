﻿using Amazon.S3;
using Amazon.S3.Model;
using BlogCore.Models.Catalogues;
using BlogCore.Models.Common;
using BlogCore.Models.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers.API
{
    [Authorize]
    public class MediaLinkController : ODataController
    {
        private readonly DatabaseContext _context;
        private readonly IAmazonS3 _s3Client;
        public MediaLinkController(DatabaseContext context, IAmazonS3 s3Client)
        {
            _context = context;
            _s3Client = s3Client;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<MediaLinkModel>>> Get()
        {
            return await _context.MediaLinks.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MediaLinkModel>> Post([FromForm]CreateMediaLinkViewModel model)
        {
            //TODO
            PutObjectRequest request = new PutObjectRequest();
            //_s3Client.PutObjectAsync()
            return Ok();
        }
    }
}
