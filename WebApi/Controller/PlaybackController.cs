using System;
using System.Collections.Generic;
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
    [Route("")]
    public class PlaybackController : ControllerBase
    {

    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;
    public PlaybackController(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
            _context = context;
    }

    [Authorize]
    [HttpPost("playback/video")]        
    public async Task<IActionResult> PlayBackVideo([FromBody] PlayBackVideoDTO input)
    {
        try
        {
            var videoDatabase = await _context.Videos.AsNoTracking().FirstAsync(x => x.Name == input.Name);
            var roleVideoDatabase = await _context.Roles.AsNoTracking().FirstAsync(x => x.id == videoDatabase.id);

            var authenticatedUser = HttpContext.User;
            var isAdmin = authenticatedUser.IsInRole("administrator");
            var isPremium = authenticatedUser.IsInRole("premium");

            if (roleVideoDatabase.Name != "standard")
            {
                if (!isAdmin && !isPremium)
                {
                    return BadRequest(new ResultDTO<User>("Conteúdo não permitido. Restrito a usuários Premium"));
                }
            }

            string videoUrl = $"http://localhost/videos/{input.Name}.mp4";

            HttpResponseMessage response = await _httpClient.GetAsync(videoUrl, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                string contentType;

                if (response.Content.Headers.ContentType != null)
                    contentType = response.Content.Headers.ContentType.ToString();
                
                else{
                    contentType = "/Video.mp4";
                }

                return File(contentStream, contentType);
            }

            else
                return NotFound(new ResultDTO<User>($"O vídeo com nome de {input.Name} não foi entrontrado"));
        }

        catch (Exception)
        {
            return StatusCode(500, new ResultDTO<Video>("01X41 - Ocorreu um erro interno ao processar sua solicitação"));
        }
    }

    }
}