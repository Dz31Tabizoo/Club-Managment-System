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
                    var query = $@"SELECT 
                                    u.UserID , 
                                    u.UserName ,
                                    u.RoleID ,
                                    u.PassWord ,
                                    r.RoleName ,
                                    u.isActive ,
                                    u.LastLogin 
                                    FROM Users u INNER JOIN Persons p ON u.UserID = p.PersonID
                                               INNER JOIN Roles r ON u.RoleID = r.RoleID                   
                                    WHERE u.UserName = @Username";
                    //result test sql: 1	admin	1	$2a$11$v9k.zYvOjrbrSTmxu9hdfOUkqzASgsJOLAxuWBZNPs.d7ILsxMA6q	ADMIN	1	2026-03-17 00:00:00.000
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

                    int affectedRows = 1; //await connection.ExecuteAsync(
                    //    "sp_UpdatePlayer",
                    //    parameters,
                    //    commandType: CommandType.StoredProcedure
                    //);
                    if (affectedRows == 0)
                    {
                        _logger.LogWarning("Mise à jour ignorée: La catégorie {Id} n'existe pas dans la base.", user.PersonID);
                        return false;
                    }
                    _logger.LogInformation("Player {Id} mise à jour avec succès.", user.PersonID);

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update User: {id} ", user.PersonID);
                throw;
            }
        }
    }
}

