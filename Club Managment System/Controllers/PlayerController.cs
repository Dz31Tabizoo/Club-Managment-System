
using CMS.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Club_Managment_System.Controllers
{
    [Route("api/Players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepo;

        public PlayerController(IPlayerRepository playerRepo)
        {
            _playerRepo = playerRepo ?? throw new ArgumentNullException(nameof(playerRepo));
        }


        [HttpGet("playersWithDetails", Name = "GetDetailsPLayers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPlayers()
        {

            var players = await _playerRepo.GetAllPlayersWithDetailsAsync();
            if (players == null) { return StatusCode(500, "Problème au connection avec le server"); }
            return Ok(players);
        }

        [HttpPost("add", Name = "AddPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerDTO playerDTO)
        {
            if (playerDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newPlayerID = await _playerRepo.AddPlayerAsync(playerDTO);
                return Ok(new { id = newPlayerID, message = "Joueur ajouté avec succès" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne s'est produite : {ex.Message}");
            }

        }



        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> UpdatePlayerAsync(int id, [FromBody] PlayerDTO player_dto)
        {
            if (player_dto == null || id != player_dto.PersonID) 
                return BadRequest("Aucun joueur information envoyé");
            

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);


            try
            {
                var result = await _playerRepo.UpdatePlayerAsync(id,player_dto);
                if (!result) return NotFound($"le Joueur avec id:{id} est introuvable.");

                return Ok(result);
                
            }
            catch (Exception ex) { return StatusCode(500, "Erreur lors de la mise à jour."); }
        }




        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            try
            {
                var result = await _playerRepo.DeletePlayerAsync(id);
                if (!result) return NotFound($"Joueur avec ID {id} n'existe pas.");

                return Ok(new { message = "Joueur désactivé (supprimé) avec succès." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erreur lors de la suppression.");
            }
        }




        [HttpGet("{id}",Name = "GetPlayerInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            var player = await _playerRepo.GetByIdAsync(id);

            if (player == null) return NotFound("Not found");
            
            return Ok(player);  
        }

    }
}
