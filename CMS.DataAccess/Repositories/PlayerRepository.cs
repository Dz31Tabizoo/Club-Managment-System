using CMS.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
    public class PlayerRepository : GenericRepository<PlayerDTO>, IPlayerRepository
    {
        public PlayerRepository(string connectionString,ILogger<PlayerRepository> logger) : base(connectionString , "Players",logger)
        {
        }

        public async Task<int> AddPlayerAsync(PlayerDTO player)
        {
            try
            {
                _logger.LogInformation("Calling sp_AddPlayer for:{FullName}", player.FullName);


                    using var connection = CreateConnection();
                return await connection.ExecuteScalarAsync<int>("sp_AddPlayer", player, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to execue  sp_AddPlayer for player: {FullName}", player.FullName);
                throw;
            }
        }
    }
}
