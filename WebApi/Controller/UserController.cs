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

        [HttpPost("create/administrator")]
        public async Task<IActionResult> CreateUserAdmin(CreateUserDTO input)
        {
            try
            {
                var userDatabase = await _context.Users.AsNoTracking().Where(x => x.Name == input.Name).ToListAsync();

                if (userDatabase.Any())
                {
                    return BadRequest(new ResultDTO<User>("Nome de usuário não disponível"));
                }

                int CountCharPassword = input.Password.Count();

                if(CountCharPassword < 6)
                {
                    return BadRequest(new ResultDTO<User>("Nome de usuário não disponível"));
                }
                
                string PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password);

                User newUserAdmin = new User();
                newUserAdmin.Name = input.Name;
                newUserAdmin.Password = PasswordHash;
                newUserAdmin.Role = "administrator";

                _context.Users.Add(newUserAdmin);
                await _context.SaveChangesAsync();
                return Ok(new ResultDTO<User>("Nome de usuário não disponível"));  
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<User>("01X24 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }
    }
}