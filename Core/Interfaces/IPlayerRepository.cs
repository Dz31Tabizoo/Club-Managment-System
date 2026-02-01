using CMS.Core.Interfaces;
using CMS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<PlayerDTO>
    {
        Task<int> AddPlayerAsync(PlayerDTO player);
        Task<IEnumerable<PlayerDTO>> GetAllPlayersWithDetailsAsync();
        Task<bool>UpdatePlayerAsync(int id,PlayerDTO player);
        Task<bool> DeletePlayerAsync(int id);

    }
}
