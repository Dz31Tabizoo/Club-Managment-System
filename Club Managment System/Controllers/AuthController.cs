using Club_Managment_System.Services;
using CMS.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace Club_Managment_System.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUsersRepository _usersRepo;

        public AuthController(TokenService tokenService, IUsersRepository usersRepository)
        {
            _tokenService = tokenService;
            _usersRepo = usersRepository;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Implement your authentication 
            // For example, validate the user's credentials and generatlogic heree a JWT token
            if (request.Username == "admin" && request.Password == "123")
            {
                //get db user and check role
                //var token = _tokenService.GenerateToken()// Role 0 for admin

                var token = "your_generated_jwt_token";
                return Ok(new LoginResponseDto
                {
                    Id = 1,
                    DisplayName = "Admin User",
                    Success = true,
                    Token = token,
                    Role = 0, // Admin
                    Message = "Connexion réussie !"
                });
            }
            return Unauthorized(new LoginResponseDto
            {
                Success = false,                
                Message = "Nom d'utilisateur ou mot de passe incorrect."
            });
        }


    }
}
