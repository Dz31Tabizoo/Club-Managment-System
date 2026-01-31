using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class PersonDTO
    {
        public int  PersonID { get; set; }
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public char? Gender { get; set; } = 'M';

    }
}
