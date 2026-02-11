using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DTOs;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CMS.DataAccess.Repositories
{
    public class ExpensesRepository : GenericRepository<ExpensesDTO>, IExpencesRepository
    {
        public ExpensesRepository(string ConnectionString, ILogger<ExpensesRepository> logger) : base(ConnectionString, "Expenses", "ExpenseID", logger)
        {
        }
    }
}
