using CMS.Core.Interfaces;
using CMS.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<UserDTO> , IUsersRepository
    {
        public UserRepository(string ConnectionString, ILogger<UserRepository> logger) : base(ConnectionString, "Users", "UserID", logger)
        {
        }

       // private readonly IConfigurationManager _configuration;  ??
        public async Task<UserDTO?> GetUserByUsernameAsync(string username)
        {
            try
            {
                using var connection = CreateConnection();
                {
                    var query = $"SELECT * FROM Users WHERE UserName = @Username";
                    return await connection.QueryFirstOrDefaultAsync<UserDTO>(query, new { Username = username });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user by username: {Username}", username);
                throw;
            }
        }


        public async Task<bool> UpdateLastLogin(int userID, UserDTO user)
        {

            try
            {
                using var connection = CreateConnection();
                {
                    var parameters = new
                    {
                        // fix userDTO to match 
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update Player: {id} ", player.PersonID);
                throw;
            }

            return true;
        
        
        }



    }
}

