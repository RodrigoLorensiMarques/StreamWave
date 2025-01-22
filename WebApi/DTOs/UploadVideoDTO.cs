using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace WebApi.DTOs
{
    public class UploadVideoDTO
    {   
        [Required (ErrorMessage = "Um arquivo com extensão .mp4 é obrigatório")]
        public IFormFile File { get; set; } 

        [Required (ErrorMessage = "Um nome é obrigatório")]
        public string Name { get; set; } 
        public string Description { get; set; }

        [Required (ErrorMessage = "Uma role é obrigatório")]
        public string Role { get; set; }
    }
}