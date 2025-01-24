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
                    return NotFound(new ResultDTO<Video>($"Não existem vídeos com nome '{name}' "));
                

                List<GetVideoDTO> videosDTO = new List<GetVideoDTO>();

                foreach (var video in videosDb)
                {
                    GetVideoDTO videoDTO = new GetVideoDTO();
                    videoDTO.Name = video.Name;
                    videoDTO.Description = video.Description;
                    videoDTO.Id = video.id;

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
                var authenticatedUser = HttpContext.User;

                List<Video> videosDb = new List<Video>();

                if(authenticatedUser.IsInRole("standard"))
                    videosDb = await _context.Videos.AsNoTracking().Where(x => x.Role.Name == "standard").Include(x => x.Role).ToListAsync();
                    
                else 
                    videosDb = await _context.Videos.AsNoTracking().ToListAsync();
                

                if (!videosDb.Any())
                    return NotFound(new ResultDTO<Video>("Não há vídeos cadastrados"));
    

                List<GetVideoDTO> videosDTO = new List<GetVideoDTO>();

                foreach (var video in videosDb)
                {
                    GetVideoDTO videoDTO = new GetVideoDTO();
                    videoDTO.Name = video.Name;
                    videoDTO.Description = video.Description;
                    videoDTO.Id = video.id;

                    videosDTO.Add(videoDTO);
                }

                return Ok(new ResultDTO<List<GetVideoDTO>>(videosDTO));
            }
            catch (Exception)
            {
               return StatusCode(500, new ResultDTO<Video>( "01X38 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }
    }
}
