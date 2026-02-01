using CMS.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
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

                var parameter = new 
                {
                    player.FirstName, player.LastName,player.DateOfBirth,player.Gender,
                    player.Email,player.Phone,player.Address,player.CategoryID,player.isActive};
                return await connection.ExecuteScalarAsync<int>("sp_AddPlayer", parameter, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to execue  sp_AddPlayer for player: {FullName}", player.FullName);
                throw;
            }
        }
    
        public async Task<IEnumerable<PlayerDTO>> GetAllPlayersWithDetailsAsync()
        {
            try
            {
                _logger.LogInformation("Getting All Players With Details");
                string sql = @"
                       SELECT p.PersonID,p.FirstName, p.LastName,p.DateOfBirth,p.Phone,
                               p.Address,p.Email,p.Email,p.Gender,pl.CategoryID,pl.isActive
                        FROM Persons p INNER JOIN Players pl
                        ON p.PersonID = PlayerID";
                using var connection = CreateConnection();
                return await connection.QueryAsync<PlayerDTO>(sql);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Failed to load Players With Details");
                throw;
            }
        }

        public async Task<bool> UpdatePlayerAsync(int id, PlayerDTO player)
        {
            using var connection = CreateConnection();
            {
                var parameters = new
                {
                    player.PersonID, 
                    player.FirstName,
                    player.LastName,
                    player.DateOfBirth,
                    player.Gender,
                    player.Email,
                    player.Phone,
                    player.Address,
                    player.CategoryID,
                    player.isActive
                };

                var affectedRows = await connection.ExecuteAsync(
                    "sp_UpdatePlayer",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return affectedRows > 0;
            }
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            using var connection = CreateConnection();
            // سنقوم فقط بتحديث حالة النشاط إلى "False"
            string sql = "UPDATE Players SET IsActive = 0 WHERE PlayerID = @id";

            var affectedRows = await connection.ExecuteAsync(sql, new { id });
            return affectedRows > 0;
        }

    }
}
