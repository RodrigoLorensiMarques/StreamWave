using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace WebApi.DTOs
{
    public class UploadVideoDTO
    {   
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}