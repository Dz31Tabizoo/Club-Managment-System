using CMS.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlTypes;

namespace CMS.DataAccess.Repositories
{
    public class GenericRepository<T> : BaseRepository<T>, IGenericRepository<T> where T : class
    {
        private readonly string _tableName;
        private readonly string _idColumn;

        public GenericRepository(string connectionString, string tableName, string idColumn, ILogger logger) : base(connectionString, logger)
        {
            _tableName = tableName;
            _idColumn = idColumn; 
        }


            // return all records of Enumerable<T> 
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all records from {TableName}", _tableName);
                using var connection = CreateConnection();
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching data from {TableName}", _tableName); throw;
            }
        }
        // return Object of <Tdto> 
        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                using var connection = CreateConnection();
                var result = await connection.QuerySingleOrDefaultAsync<T>(
                    sql: $"SELECT * FROM {_tableName} WHERE {_idColumn} = @Id", new { Id = id });
                if (result == null)
                {
                    _logger.LogError("record with ID {Id} Not found in Table {TableName}", id, _tableName);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching record with ID {Id} from {TableName}", id, _tableName);
                throw;
            }
        }

        
    }

}
