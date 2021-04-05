﻿using BlogCore.Models.Catalogues;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class CreateMediaLinkViewModel
    {
        public string Name { get; set; }
        public Guid Group { get; set; }
        public IFormFile File { get; set; }
    }
}
