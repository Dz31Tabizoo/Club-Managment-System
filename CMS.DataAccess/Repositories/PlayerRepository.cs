using CMS.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
    public class PlayerRepository : GenericRepository<PlayerDTO>, IPlayerRepository
    {
        public PlayerRepository(string connectionString,ILogger<PlayerRepository> logger) : base(connectionString, "Players", "PlayerID",logger) { }


        // we should add player details + Attendance || player details + subscription


        public async Task<int> AddPlayerAsync(PlayerDTO player)
        {
            try
            {
                _logger.LogInformation("Calling sp_AddPlayer for:{FullName}", player.FullName);


                    using var connection = CreateConnection();

                var parameter = new 
                {
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

                return await connection.ExecuteScalarAsync<int>("sp_AddPlayer", parameter, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to execue sp_AddPlayer for player: {FullName}", player.FullName);
                throw;
            }
        }
    
        public async Task<IEnumerable<PlayerDTO>> GetAllPlayersWithDetailsAsync()
        {
            try
            {
                _logger.LogInformation("Getting All Players With Person Details.");
                string sql = @"
                      SELECT 
                              p.PersonID,
                              p.FirstName,
                              p.LastName,
                              p.DateOfBirth,
                              p.Phone,
                              p.Address,
                              p.Email,
                              p.Gender,
                              pl.CategoryID,
                              pl.isActive
                      FROM 
                              Persons p INNER JOIN Players pl
                        ON    p.PersonID = pl.PlayerID";

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
        { try
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
                    if (affectedRows == 0)
                    {
                        _logger.LogWarning("Mise à jour ignorée: La catégorie {Id} n'existe pas dans la base.", player.PersonID);
                        return false;
                    }
                    _logger.LogInformation("Player {Id} mise à jour avec succès.", player.PersonID);
                    
                    return affectedRows > 0;
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to update Player: {id} ",player.PersonID);
                throw;
            }
        }        

      
    }
}
