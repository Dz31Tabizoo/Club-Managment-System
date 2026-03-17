using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class UserDTO : PersonDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        [Required]
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;

        public DateTime? LastLogin { get; set; }
    }
}
