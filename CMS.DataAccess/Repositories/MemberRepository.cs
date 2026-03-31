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
    public class MemberRepository : BaseRepository<dynamic> , IMemberRepository
    {
        public MemberRepository(string ConnectionString, ILogger<MemberRepository> logger) : base(ConnectionString, logger)
        {
        }

        public async Task<IEnumerable<dynamic>> GetAllMembersAsync()
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    var result = await connection.QueryAsync<dynamic>(
                        "sp_GetAllMembers", commandType: CommandType.StoredProcedure
                        );
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all members");
                throw;
            }
        }

    }
}
