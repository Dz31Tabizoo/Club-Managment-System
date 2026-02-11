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
    public class UserRepository : GenericRepository<UserDTO> , IUsersRepository
    {
        public UserRepository(string ConnectionString, ILogger<UserRepository> logger) : base(ConnectionString, "Users", "UserID", logger)
        {
        }
    }
}

