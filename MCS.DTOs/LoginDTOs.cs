using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public record LoginRequestDto(string Username, string Password);

    
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public int Role { get; set; } 
    }
}
