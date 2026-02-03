using CMS.DTOs;
using Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryDTO> , ICategoryRepository
    {

        public CategoryRepository(string ConnectionString, ILogger<CategoryRepository> logger ) : base( ConnectionString,"Categories","CategoryID", logger ) { }

        //get all categories : From GenericRepository 
        public async Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO )
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    var parameters = new
                    {
                        categoryID = categoryDTO.CategoryID, 
                        categoryName = categoryDTO.CategoryName, 
                        minAge = categoryDTO.MinAge,
                        maxAge = categoryDTO.MaxAge,
                        Fee = categoryDTO.MonthlyFee 
                    };

                    int rowAffected = await connection.ExecuteAsync("sp_UpdateCategory", parameters, commandType: CommandType.StoredProcedure);
                    if (rowAffected > 0)
                    {
                        _logger.LogInformation("Catégorie {Id} mise à jour avec succès.", categoryDTO.CategoryID);
                        return true;
                    }  
                    
                    return false;
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Erreur SQL lors de la mise à jour de la catégorie {Id}. Détails: {Message}", categoryDTO.CategoryID, sqlEx.Message);
                throw;
                
            }

        }




    }
}
