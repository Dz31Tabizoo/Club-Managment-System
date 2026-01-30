using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class OtherIncomeDTO
    {
        public int IncomeID { get; set; }
        public string SourceTitle { get; set; }
        public decimal Amount { get; set; }
        public DateTime IncomeDate { get; set; }
        public string Notes { get; set; }

        // لربط الدخل بالشخص الذي استلمه (الموظف أو الأدمن)
        public int? ReceivedByUserID { get; set; }
        public string ReceivedByUserName { get; set; } // لعرض اسم المستخدم في التقارير
    }
}
