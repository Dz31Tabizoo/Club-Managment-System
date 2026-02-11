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
    public class OtherIncomesRepository : GenericRepository<OtherIncomeDTO> , IOtherIncomesRepository
    {
        public OtherIncomesRepository(string ConnectionString, ILogger<OtherIncomesRepository> logger) : base(ConnectionString, "OtherIncomes", "IncomeID", logger)
        {
        }
    }
}
