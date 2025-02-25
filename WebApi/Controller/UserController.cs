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
                var userDatabase = await _context.Users.AsNoTracking().Include(x => x.Role).SingleOrDefaultAsync(x => x.Name == input.Name);

                if (userDatabase == null)
                    return StatusCode(401, new ResultDTO<User>("Credenciais incorretas ou usuário não existe"));

                bool verified = BCrypt.Net.BCrypt.Verify(input.Password, userDatabase.Password);

                 if (verified == true)
                 {
                     var token = _tokenService.GenerateJwtToken(userDatabase.Name, userDatabase.id, userDatabase.Role.Name);

                     return Ok(new ResultDTO<List<string>>(new List<string>() {"Acesso Liberado", token}, null));
                 }

                return StatusCode(401, new ResultDTO<User>("Credenciais incorretas ou usuário não existe"));

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
                var userDatabase = await _context.Users.AsNoTracking().Where(x => x.Name == input.Name).ToListAsync();

                if (userDatabase.Any())
                    return BadRequest(new ResultDTO<User>("Nome de usuário não disponível"));

                string PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password);

                var roleDatabase = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == input.Role);

                if (roleDatabase == null)
                    return NotFound(new ResultDTO<User>($"Role '{input.Role}' não existe"));


                var authenticatedUser = HttpContext.User;

                if (authenticatedUser.Identity.IsAuthenticated == true)
                {
                    var isAdmin = authenticatedUser.IsInRole("administrator");

                    if (!isAdmin && input.Role != "standard")
                        return StatusCode(401, new ResultDTO<User>("Apenas administradores podem criar usuários com role superior a standard"));
                }

                else
                {
                    if (input.Role != "standard")
                        return StatusCode(401, new ResultDTO<User>("Role disponível apenas para administradores logados"));
                }

                User newUser = new User();
                newUser.Name = input.Name;
                newUser.Password = PasswordHash;
                newUser.RoleId = roleDatabase.id;

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Ok(new ResultDTO<string>("Usuário cadastrado com sucesso!", null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultDTO<User>("01X24 - Ocorreu um erro interno ao processar sua solicitação"));
            }
        }
    }
}
