using Club_Management_System.Controllers;
using CMS.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;

namespace Club_Managment_System.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : BaseController<CategoryDTO, ICategoryRepository>
    {
        
        //contructor
        public CategoryController(ICategoryRepository categoryRepo, ILogger<CategoryController> logger) : base(categoryRepo, logger)
        {
            
        }

        
        
        [HttpPut("UpdateCategory", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Données de catégorie invalides");
                }
                var editedCategory = await _repository.UpdateCategoryAsync(category);

                if (!editedCategory)
                {
                    return StatusCode(500, "Erreur a la base de donnes.");
                }
                return Ok();

            }
            catch
            {
                return StatusCode(500, "Problème interne.");
            }
        }



        


    }
}
