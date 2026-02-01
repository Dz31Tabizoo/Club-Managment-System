
using CMS.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Managment_System.Controllers
{
    [Route("api/Players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepo;

        public PlayerController(IPlayerRepository playerRepo)
        {
            _playerRepo = playerRepo;
        }


        [HttpGet("playersWithDetails",Name = "GetDetailsPLayers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPlayers()
        {
                        
            var players = await _playerRepo.GetAllPlayersWithDetailsAsync();
            if (players == null) { return StatusCode(500,"Problème au connection avec le server"); }
            return Ok(players);
        }






        [HttpPost("add",Name = "AddPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>CreatePlayer([FromBody]PlayerDTO playerDTO)
        {
            if (playerDTO == null || !ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                var newPlayerID = await _playerRepo.AddPlayerAsync(playerDTO);
                return Ok(new { id = newPlayerID, message = "Joueur ajouté avec succès" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Une erreur interne s'est produite.");
            }

        }

    }
}
