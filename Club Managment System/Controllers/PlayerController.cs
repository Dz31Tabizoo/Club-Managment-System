using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepo;

        public PlayerController(IPlayerRepository playerRepo)
        {
            _playerRepo = playerRepo;
        }

    }
}
