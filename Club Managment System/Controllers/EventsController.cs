using Club_Management_System.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CMS.DTOs;
using Core.Interfaces;

namespace Club_Managment_System.Controllers
{
    [Route("api/Events")]
    [ApiController]
    public class EventsController : BaseController<EventDto, IEventsRepository>
    {
        public EventsController(IEventsRepository repository, ILogger<EventsController> logger) : base(repository, logger)
        {


        }

    }
}
