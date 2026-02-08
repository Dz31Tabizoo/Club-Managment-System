using CMS.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Managment_System.Controllers
{
    [Route("api/Coach")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachRepository _coachRepo;

        public CoachController(ICoachRepository coachRepo)
        {
            _coachRepo = coachRepo;
        }

        [HttpGet("Coachs", Name = "GetAllCoachs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var coachs = await _coachRepo.GetAllAsync();
                if (coachs == null || !coachs.Any())
                {
                    return NotFound("Aucune catégorie trouvée");
                }
                return Ok(coachs);
            }
            catch 
            {
                return StatusCode(500, "Problème interne.");
            }
        }



        [HttpPut("UpdateCoach")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCoach([FromBody] CoachDTO Coach)
        {
            try
            {
                if (Coach == null)
                {
                    return BadRequest("Données de catégorie invalides");
                }
                var editedCoach = await _coachRepo.UpdateCoachAsync(Coach);

                if (!editedCoach)
                {
                    return StatusCode(500, "Erreur a la base de donnes.");
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Problème interne.");
            }
        }


    }
