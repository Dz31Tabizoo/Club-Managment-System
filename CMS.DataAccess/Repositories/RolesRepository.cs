using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DTOs;
using Core.Interfaces;
using MCS.DTOs;
using CMS.Core.Interfaces;

namespace CMS.DataAccess.Repositories
{
    public class RolesRepository : GenericRepository<RolesDTO>, IRoleRepository
    {
        public RolesRepository(string ConnectionString, ILogger<RolesRepository> logger) : base(ConnectionString, "Roles", "RoleID", logger)
        {

        }
    }
}
