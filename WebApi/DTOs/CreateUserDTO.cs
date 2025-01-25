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
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required (ErrorMessage ="Senha é obrigatório"), MinLength(6, ErrorMessage = "Senha deve possuir pelo menos 6 caracteres")]
        public string Password { get; set; }

        [Required (ErrorMessage ="Role é obrigatória")]
        public string Role { get; set; }
    }
}