using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Data;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PlaybackController : ControllerBase
    {

    private readonly HttpClient _httpClient;
    public PlaybackController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    [HttpGet]        
    public async Task<IActionResult> PlayBackVideo(string name)
    {
        try
        {
            string videoUrl = $"http://localhost/videos/{name}.mp4";

            HttpResponseMessage response = await _httpClient.GetAsync(videoUrl, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                string contentType;

                if (response.Content.Headers.ContentType != null)
                {
                        contentType = response.Content.Headers.ContentType.ToString();
                }

                else{
                        contentType = "/Video.mp4";
                }

                return File(contentStream, contentType);
            }

            else
            {
                return NotFound("Item não encontrado");
            }
        }
        
        catch (Exception)
        {

            return StatusCode(500, "01X41 - Ocorreu um erro interno ao processar sua solicitação");
        }
    }








    }
}