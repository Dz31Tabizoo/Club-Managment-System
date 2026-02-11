using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Core.Interfaces;
using CMS.DTOs;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CMS.DataAccess.Repositories
{
    public class PlayerAttendanceRepository : GenericRepository<PlayerAttendanceDTO>, IPlayerAttendanceRepository
    {
        public PlayerAttendanceRepository(string ConnectionString, ILogger<PlayerAttendanceRepository> logger) : base(ConnectionString, "PlayerAttendance", "AttendanceID", logger)
        {

        }
    }
}
