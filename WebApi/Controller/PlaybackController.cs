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

        [HttpGet]        
        public IActionResult PlayBackVideo(string name)
        {
             string videoDirectory = "http://localhost/videos/Listen+.mp4";

             return Redirect(videoDirectory);
        }


        


    }
}