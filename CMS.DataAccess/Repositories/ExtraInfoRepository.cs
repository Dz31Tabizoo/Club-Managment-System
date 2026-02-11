using CMS.DTOs;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DataAccess.Repositories
{
   
    public class ExtraInfoRepository : GenericRepository<ExpensesDTO>, IExpencesRepository
    {
        public ExtraInfoRepository(string ConnectionString, ILogger<ExtraInfoRepository> logger) : base(ConnectionString, "ExtraInfo", "PersonID", logger)
        {
        }
    }
}
