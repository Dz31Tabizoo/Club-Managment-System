using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public decimal MonthlyFee { get; set; }
    }
}
