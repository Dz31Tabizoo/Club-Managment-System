using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManagementSystem.Models
{
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public int Role { get; set; }

    }
}
