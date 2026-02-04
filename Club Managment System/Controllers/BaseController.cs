using CMS.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T, TRepo> : ControllerBase where TRepo : IGenericRepository<T> where T: class
    {
        protected readonly TRepo _repository;
        protected readonly ILogger<BaseController<T, TRepo>> _logger;


        protected BaseController(TRepo repository,ILogger<BaseController<T, TRepo>> logger)
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













    }



    
}
