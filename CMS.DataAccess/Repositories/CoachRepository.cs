using CMS.Core.Interfaces;
using CMS.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
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
    public class CoachRepository : GenericRepository<CoachDTO> , ICoachRepository
    {
        public CoachRepository(string connectionString, ILogger<CoachRepository> logger) : base(connectionString, "Coaches", "CoacheID", logger)
        {

        }
        public async Task<bool> UpdateCoachAsync(CoachDTO coach)
        {
            try
            {
                _logger.LogInformation("Calling sp_UpdateCoach for:{FullName}", coach.FullName);
                using var connection = CreateConnection();
                var parameter = new
                {
                    coach.PersonID,
                    coach.FirstName,
                    coach.LastName,
                    coach.DateOfBirth,
                    coach.Address,
                    coach.Email,
                    coach.Phone,
                    coach.Specialization,
                    coach.salary,
                    coach.isActive,
                    coach.Gender
                };

                int affectedRows = await connection.ExecuteAsync("sp_UpdateCoach", parameter, commandType: CommandType.StoredProcedure);

                if (affectedRows == 0)
                {
                    _logger.LogWarning("Mise à jour ignorée: La Coach {CoachName} n'existe pas dans la base.", coach.FirstName);
                    return false;
                }
                _logger.LogInformation("Coach {Id} mise à jour avec succès.", coach.PersonID);

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update Player: {id} ", coach.PersonID);
                throw;
            }
        }
    }
}