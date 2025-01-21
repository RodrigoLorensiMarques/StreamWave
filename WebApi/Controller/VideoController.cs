using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Entities;

namespace WebApi.Controller
{   
    [ApiController]
    [Route("")]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly AppDbContext _context;


        public VideoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("videos")]
        public async Task<IActionResult> GetVideosByName([Required]string name)
        {
            try
            {
                var videosDb = await _context.Videos.AsNoTracking().Where(x => x.Name.Contains(name)).ToListAsync();

                if (!videosDb.Any())
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
                var videosDb = await _context.Videos.AsNoTracking().ToListAsync();

                if (!videosDb.Any())
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

        
        [Authorize(Roles ="administrator")]
        [HttpPost("videos/upload")]
        public async Task<IActionResult> UploadVideo([FromForm]UploadVideoDTO input)
        {
            try
            {
                var videoDb = await _context.Videos.AsNoTracking().FirstOrDefaultAsync(x => x.Name == input.Name);

                 if (videoDb != null)
                 {
                     return BadRequest("Um vídeo com esse nome já foi cadastrado");
                 }

                 Video newVideo = new Video();
                 newVideo.Name = input.Name;
                 newVideo.Description = input.Description;
                 newVideo.DateAdd = DateTime.Now;

                 await _context.Videos.AddAsync(newVideo);
                 await _context.SaveChangesAsync();


                string fileExtension = Path.GetExtension(input.File.FileName);

                if (!fileExtension.Contains(".mp4"))
                    return BadRequest("Tipo de arquivo não suportado");

                string videosPath = "C:/nginx/var/www/videos";

                input.Name = input.Name + ".mp4";

                string filePath = Path.Combine(videosPath, input.Name);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await input.File.CopyToAsync(stream);
                    }

                return Ok(new ResultDTO<UploadVideoDTO>($"Arquivo {input.Name} enviado com sucesso"));
            }
            catch (Exception)
            {
               return StatusCode(500, new ResultDTO<Video>( "01X89 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }
    }
}
