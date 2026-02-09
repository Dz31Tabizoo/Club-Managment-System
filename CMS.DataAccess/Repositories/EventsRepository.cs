using CMS.DTOs;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
    public class EventsRepository : GenericRepository<EventDto>, IEventsRepository
    {
        public EventsRepository(string ConnectionString, ILogger<EventsRepository> logger) : base(ConnectionString, "Events", "EventID", logger)
        {
        }
    }
}
