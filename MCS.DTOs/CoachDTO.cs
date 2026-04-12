using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class CoachDTO : PersonDTO
    {
        public int CoachID { get; set; }
        public string? Specialization { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
    }

}
