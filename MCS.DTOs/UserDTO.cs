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
        public string UserName { get; set; }
        public string? Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;

        public DateTime? LastLogin { get; set; }
    }
}
