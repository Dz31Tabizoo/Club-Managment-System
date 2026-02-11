using CMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DTOs;

namespace Core.Interfaces
{
    public interface IExpencesRepository : IGenericRepository<ExpensesDTO>
    {
    }
}
