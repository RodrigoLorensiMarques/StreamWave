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
    public class StreamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StreamController(AppDbContext context)
        {
            _context = context;
        }


        


    }
}