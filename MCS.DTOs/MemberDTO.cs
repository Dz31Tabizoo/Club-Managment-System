using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class MemberDTO
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }        
        
        public string? Gender { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? PlayerID { get; set; }
        public int? CategoryID { get; set; }
        public string? CategoryName { get; set; }

        public int? CoachID { get; set; }
        public string? Specialization { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; }

    }
}
