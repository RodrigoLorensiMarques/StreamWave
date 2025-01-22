using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class AcessUserDTO
    {   
        [Required (ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }
    }
}