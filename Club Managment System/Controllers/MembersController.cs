using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.DTOs;
using Core.Interfaces;


namespace Club_Managment_System.Controllers
{
    [Authorize]
    [Route("api/Members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberServices _memberServices;
        private readonly ILogger<MembersController> _logger;

        public MembersController (IMemberServices memberServices, ILogger<MembersController> logger)
        {
            _memberServices = memberServices;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Getting all members");
            
        
            var members = await _memberServices.GetAllMembersAsync();
            if (members == null)
            {
                return Ok(new List<PersonDTO>());
            }
                return Ok(members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all members");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
