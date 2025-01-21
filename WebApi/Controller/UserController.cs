using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controller
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public UserController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Acess(AcessUserDTO input)
        {
            try
            {
                var userDatabase = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Name == input.Name);

                if (userDatabase != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(input.Password, userDatabase.Password);

                    if (verified == true)
                    {
                        var token = _tokenService.GenerateJwtToken(userDatabase.Name, userDatabase.id, userDatabase.Role);
                        return Ok(new {message="Acesso Liberado", token });
                    }
                    return BadRequest(new ResultDTO<User>("Credenciais incorretas ou usuário não existe"));
                }
                else 
                {
                    return BadRequest(new ResultDTO<User>("Credenciais incorretas ou usuário não existe"));
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<User>("01X35 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }

        [HttpPost("user/create")]
        public async Task<IActionResult> CreateUserAdmin(CreateUserDTO input)
        {
            try
            {
                List<string> roles = new List<string>{"standard", "premium", "administrator"};


                var userDatabase = await _context.Users.AsNoTracking().Where(x => x.Name == input.Name).ToListAsync();

                if (userDatabase.Any())
                {
                    return BadRequest(new ResultDTO<User>("Nome de usuário não disponível"));
                }

                int CountCharPassword = input.Password.Count();

                if(CountCharPassword < 6)
                {
                    return BadRequest(new ResultDTO<User>("Senha deve possuir pelo menos 6 caracteres"));
                }
                
                string PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password);


                var authenticatedUser = HttpContext.User;

                if (authenticatedUser.Identity.IsAuthenticated == true)
                {
                    var isAdmin = authenticatedUser.IsInRole("administrator");

                    if (!isAdmin && input.Role!= "standard")
                    {
                        return BadRequest(new ResultDTO<User>("Apenas administradores podem criar usuários com role superior a standard"));
                    }

                    else if (isAdmin && !roles.Contains(input.Role))
                    {
                        return BadRequest(new ResultDTO<User>("Role não existe"));
                    }

                }

                else
                {   
                    if (input.Role != "standard")
                    {
                        return BadRequest(new ResultDTO<User>("Role não disponível para usuários não logados"));
                    }
                }


                User newUser = new User();
                newUser.Name = input.Name;
                newUser.Password = PasswordHash;
                newUser.Role = input.Role;

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Ok(new ResultDTO<User>("Usuário cadastrado com sucesso!"));  
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<User>("01X24 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }
    }
}