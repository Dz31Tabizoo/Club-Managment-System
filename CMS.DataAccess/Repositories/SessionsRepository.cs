using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.DTOs;
using Microsoft.Extensions.Logging;
using CMS.DTOs;

namespace CMS.DataAccess.Repositories
{
    public class SessionsRepository : GenericRepository<SessionDTO>, ISessionsRepository
    {
        public SessionsRepository(string ConnectionString, ILogger<SessionsRepository> logger) : base(ConnectionString, "Sessions", "SessionID", logger)
        {
        }
    }
}
