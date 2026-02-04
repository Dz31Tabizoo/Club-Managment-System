using CMS.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;

namespace Club_Managment_System.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoriesRepo;
        //contructor
        public CategoryController(ICategoryRepository categories)
        {
            _categoriesRepo = categories;
        }

        
        [HttpGet("Categories", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoriesRepo.GetAllAsync();
                if (categories == null || !categories.Any())
                {
                    return NotFound("Aucune catégorie trouvée");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Problème interne.");
            }

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
                var editedCategory = await _categoriesRepo.UpdateCategoryAsync(category);

                if (!editedCategory)
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


        [HttpGet("Category/{id}", Name = "GetCategoryByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {

                var category = await _categoriesRepo.GetByIdAsync(id);

                if (category == null) return NotFound("Not found");

                return Ok(category);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Problème interne");
            }
        }


    }
}
