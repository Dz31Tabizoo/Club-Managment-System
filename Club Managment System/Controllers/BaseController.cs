using CMS.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Management_System.Controllers
{
    [Route("api/Base")]
    [ApiController]
    public abstract class BaseController<T, TRepo> : ControllerBase where TRepo : IGenericRepository<T> where T: class
    {
        protected readonly TRepo _repository;
        protected readonly ILogger _logger;


        protected BaseController(TRepo repository,ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("toggleActivation/{id}")] // for small edits
        public virtual async Task<IActionResult> ToggleActivation(int id)
        {
            try
            {
                var result = await _repository.ToggleActivationAsync(id);

                if (!result)
                {
                    return BadRequest("Echec de la modification du statut.");
                }


                return Ok(new { message = "Status mis à jour avec succès:" });


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne s'est produite : {ex.Message}");
            }
        }


        [HttpGet("GetEntity/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);

            if (item == null) return NotFound("Not found");

            return Ok(item);
        }


        [HttpGet("GetData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var categories = await _repository.GetAllAsync();
                if (categories == null || !categories.Any())
                {
                    return NotFound("Aucune catégorie trouvée");
                }
                return Ok(categories);
            }
            catch
            {
                return StatusCode(500, "Problème interne.");
            }

        }




    }    
}
