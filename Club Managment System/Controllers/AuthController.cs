using Club_Managment_System.Services;
using CMS.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;

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
            // 1. Récupérer l'utilisateur
            var user = await _usersRepo.GetUserByUsernameAsync(request.Username);

            // 2. Vérification : Utilisateur inexistant OU Mot de passe incorrect
            // Note : On utilise BCrypt.Verify (sans le !) pour valider que ça CORRESPOND
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(new LoginResponseDto
                {
                    Success = false,
                    Message = "Nom d'utilisateur ou mot de passe incorrect."
                });
            }

            // 3. Si on arrive ici, l'authentification est réussie
            var token = _tokenService.GenerateToken(user.PersonID, user.RoleId);

            return Ok(new LoginResponseDto
            {
                Id = user.PersonID,
                DisplayName = user.FullName,
                Success = true,
                Token = token,
                Role = user.RoleId,
                Message = "Connexion réussie !"
            });
        }


    }
}
