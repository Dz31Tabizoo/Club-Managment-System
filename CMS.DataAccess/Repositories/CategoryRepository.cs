using CMS.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryDTO> , ICategories
    {

        public CategoryRepository(string ConnectionString, ILogger<CategoryRepository> logger ) : base( ConnectionString,"Categories","CategoryID", logger ) { } 
        

        
        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            try
            {
                using var connection = CreateConnection();
                string sql = "SELECT CategoryID,CategoryName,MonthlyFee FROM Categories";

                return await connection.QueryAsync<CategoryDTO>(sql);

            }
            catch(Exception exc)
            {
                _logger.LogError(exc, "Failed to load Categories.");
                throw;
            }
        }




    }
}
