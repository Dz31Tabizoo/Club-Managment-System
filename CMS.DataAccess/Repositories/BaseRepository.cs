using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected readonly string _connectionString;
        protected readonly ILogger _logger;

        protected BaseRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        protected IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        
    }
}
