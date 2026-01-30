using CMS.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CMS.DataAccess.Repositories
{
    public class GenericRepository<T> : BaseRepository<T>, IGenericRepository<T> where T : class
    {
        private readonly string _tableName;
        public GenericRepository(string connectionString, string tableName, ILogger<T> logger) : base(connectionString, logger)
        {
            _tableName = tableName;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to retrive all records from {TableName}", _tableName);
                using var connection = CreateConnection();
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while fetching data from {TableName}", _tableName); throw;
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<T>(
                    $"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching record with ID {Id} from {TableName}", id, _tableName);
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(
                    $"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting record with ID {Id} from {TableName}", id, _tableName);
                throw;

            }



}
