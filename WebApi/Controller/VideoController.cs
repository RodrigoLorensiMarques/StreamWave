using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Entities;

namespace WebApi.Controller
{   
    [ApiController]
    [Route("")]
    public class VideoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public VideoController(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpGet("videos")]
        public async Task<IActionResult> GetVideosByName([Required]string name)
        {
            try
            {
                var videosDb = await _context.Videos.Where(x => x.Name.Contains(name)).ToListAsync();

                if (videosDb.IsNullOrEmpty())
                {
                    return NotFound(new ResultDTO<Video>($"Não existem vídeos com nome de '{name}' "));
                }

                List<GetVideoDTO> videosDTO = new List<GetVideoDTO>();

                foreach (var video in videosDb)
                {
                    GetVideoDTO videoDTO = new GetVideoDTO();
                    videoDTO.Name = video.Name;
                    videoDTO.Description = video.Description;

                    videosDTO.Add(videoDTO);
                }

                return Ok(new ResultDTO<List<GetVideoDTO>>(videosDTO));
            }
            catch (Exception)
            {

                return StatusCode(500, new ResultDTO<Video>("01X37 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }


        [HttpGet("videos/get-all")]
        public async Task<IActionResult> GetAllVideos()
        {
            try
            {
                var videosDb = await _context.Videos.ToListAsync();

                if (videosDb.IsNullOrEmpty())
                {
                    return NotFound(new ResultDTO<Video>("Não há vídeos cadastrados"));
                }

                List<GetVideoDTO> videosDTO = new List<GetVideoDTO>();

                foreach (var video in videosDb)
                {
                    GetVideoDTO videoDTO = new GetVideoDTO();
                    videoDTO.Name = video.Name;
                    videoDTO.Description = video.Description;

                    videosDTO.Add(videoDTO);
                }

                return Ok(new ResultDTO<List<GetVideoDTO>>(videosDTO));
            }
            catch (Exception)
            {
                
                return StatusCode(500, new ResultDTO<Video>( "01X38 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }


        [HttpPost("videos/upload")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);


            if (!fileExtension.Contains(".mp4"))
                return BadRequest("Tipo de arquivo não suportado");

            string videosPath = "C:/nginx/var/www/videos";

            string filePath = Path.Combine(videosPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            return Ok($"Arquivo {file.FileName} enviado com sucesso");
        }
    }
}
