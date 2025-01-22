using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DTOs
{
    public class CreateUserDTO
    {   
        [Required (ErrorMessage ="Nome é obrigatório")]
        public string Name { get; set; }

        [Required (ErrorMessage ="Senha é obrigatório")]
        public string Password { get; set; }

        [Required (ErrorMessage ="Role é obrigatório")]
        public string Role { get; set; }
    }
}