using CMS.Core.Interfaces;
using CMS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUsersRepository : IGenericRepository<UserDTO>
    {
         Task<UserDTO?> GetUserByUsernameAsync(string username);
         Task<bool> UpdateLastLogin(int userID, UserDTO user);
    }
}
