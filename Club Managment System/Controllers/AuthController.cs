using Club_Managment_System.Services;
using CMS.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using BCrypt.Net;

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
            var user = await _usersRepo.GetUserByUsernameAsync(request.Username);
            // Implement your authentication 
            // For example, validate the user's credentials and generatlogic heree a JWT token
            if (user != null || !BCrypt.Net.BCrypt.Verify(request.Password,user.Password))
            {
                //get db user and check role

                return Unauthorized(new LoginResponseDto
                {
                    Success = false,
                    Message = "Nom d'utilisateur ou mot de passe incorrect."
                });
            }

            var token = _tokenService.GenerateToken(user.PersonID, user.RoleId); // Role 0 for admin

            return Unauthorized(new LoginResponseDto
            {
                Id = user.PersonID,
                DisplayName = user.FullName,
                Success = true,
                Token = token,
                Role = user.RoleId
            }
            );
        }


    }
}
