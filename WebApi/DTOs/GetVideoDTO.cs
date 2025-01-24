using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class GetVideoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}