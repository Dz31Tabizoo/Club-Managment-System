using MCS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<RolesDTO> GetRoles();

        RolesDTO GetRoleByID(int id);

        int Add(RolesDTO role);

        bool Update(RolesDTO role);

        bool Delete(RolesDTO role);

    }
    
}
