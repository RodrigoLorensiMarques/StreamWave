using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Entities;

namespace WebApi.Controller
{   
    [ApiController]
    [Route("manager")]
    [Authorize(Roles ="administrator")]
    public class ManagerController : ControllerBase
    {
        public readonly AppDbContext _context;
        public ManagerController(AppDbContext context)
        {
            _context = context;   
        }

        [HttpPost("videos/upload")]
        public async Task<IActionResult> UploadVideo([FromForm]UploadVideoDTO input)
        {
            try
            {
                string fileExtension = Path.GetExtension(input.File.FileName);

                if (!fileExtension.Contains(".mp4"))
                    return BadRequest(new ResultDTO<User>($"Tipo de arquivo com extensão {fileExtension} não suportado"));
                
                var roleDatabase = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == input.Role);

                if (roleDatabase == null)
                    return NotFound(new ResultDTO<User>($"Role '{input.Role}' não existe"));

                 Video newVideo = new Video();
                 newVideo.Name = input.Name;
                 newVideo.Description = input.Description;
                 newVideo.DateAdd = DateTime.Now;
                 newVideo.RoleId = roleDatabase.id;

                 await _context.Videos.AddAsync(newVideo);
                 await _context.SaveChangesAsync();

                var newVideoId = newVideo.id;

                string videosPath = Path.Combine(Directory.GetCurrentDirectory(), "..","nginx","data", "www", "videos");
                string videoNameDirectory = newVideoId + ".mp4";
                string filePath = Path.Combine(videosPath, videoNameDirectory);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await input.File.CopyToAsync(stream);
                    }

            return Ok(new ResultDTO<List<string>>(new List<string>() {$"Arquivo {input.Name} enviado com sucesso",$"id: {newVideoId.ToString()}"}, null));
            }
            catch (Exception)
            {
               return StatusCode(500, new ResultDTO<Video>( "01X89 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }

        
        [HttpDelete("videos")]
        public async Task<IActionResult> DeleteVideo([Required]int id)
        {
            try
            {
                var videoDatabase = await _context.Videos.FirstOrDefaultAsync(x => x.id == id);

                if (videoDatabase == null)
                    return NotFound(new ResultDTO<Video>($"Vídeo com id {id} não localizado"));

                _context.Videos.Remove(videoDatabase);
                await _context.SaveChangesAsync();

                string videosPath = Path.Combine(Directory.GetCurrentDirectory(), "..","nginx","data", "www", "videos");
                videoDatabase.Name = videoDatabase.Name + ".mp4";
                string filePath = Path.Combine(videosPath, videoDatabase.Name);

                System.IO.File.Delete(filePath);

                return Ok(new ResultDTO<string>($"Vídeo id {id} deletado com sucesso",null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<Video>("01X89 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }
    }
}