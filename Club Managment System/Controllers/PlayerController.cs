
using Club_Management_System.Controllers;
using CMS.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Club_Management_System.Controllers
{
    [Route("api/Players")]
    [ApiController]
    public class PlayerController : BaseController<PlayerDTO,IPlayerRepository>
    {

        public PlayerController(IPlayerRepository playerRepo,ILogger<PlayerController> logger): base (playerRepo,logger)
        {
        }


        [HttpGet("playersWithDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPlayers()
        {

            var players = await _repository.GetAllPlayersWithDetailsAsync();
            if (players == null) { return StatusCode(500, "Problème au connection avec le server"); }
            return Ok(players);
        }


        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerDTO playerDTO)
        {
            if (playerDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var newPlayerID = await _repository.AddPlayerAsync(playerDTO);
                return Ok(new { id = newPlayerID, message = "Joueur ajouté avec succès" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur Lors de l'ajout d'un joueur");
                return StatusCode(500, $"Erreur interne");
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
                var result = await _repository.UpdatePlayerAsync(id,player_dto);
                if (!result) return NotFound($"le Joueur avec id:{id} est introuvable.");

                return Ok(result);
                
            }
            catch { return StatusCode(500, "Erreur lors de la mise à jour."); }
        }




        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            return await base.ToggleActivation(id);
        }




        [HttpGet("{id}",Name = "GetPlayerInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlayerById(int id)
        { 
            return await base.GetByIdAsync(id);
        }

    }
}
