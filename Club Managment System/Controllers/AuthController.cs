using CMS.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Managment_System.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Implement your authentication 
            // For example, validate the user's credentials and generatlogic heree a JWT token
            if (request.Username == "admin" && request.Password == "123")
            {
                // Generate a JWT token (this is just a placeholder, implement proper token generation)
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
            return Unauthorized();
        }


    }
}
