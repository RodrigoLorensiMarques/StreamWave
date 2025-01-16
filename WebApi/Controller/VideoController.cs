using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data;
using WebApi.DTOs;

namespace WebApi.Controller
{   
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VideoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVideo(string name)
        {
            try
            {
                var videos = await _context.Videos.Where(x => x.Name == name).ToListAsync();

                if (videos.IsNullOrEmpty())
                {
                    return NotFound($"Não existem vídeos com nome de '{name}' ");
                }
                
                return Ok(videos);
            }
            catch (Exception)
            {

                return StatusCode(500, "01X37 - Ocorreu um erro interno ao processar sua solicitação");
            }
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVideos()
        {
            try
            {
                var videos = await _context.Videos.ToListAsync();

                if (videos.IsNullOrEmpty())
                {
                    return NotFound("Não há vídeos cadastrados");
                }

                return Ok(videos);
            }
            catch (Exception)
            {
                
                return StatusCode(500, "01X38 - Ocorreu um erro interno ao processar sua solicitação");
            }
        }


        


    }
}