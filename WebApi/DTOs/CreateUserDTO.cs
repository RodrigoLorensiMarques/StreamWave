using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DTOs
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}