using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManagementSystem.Models
{
    public class LoginRequestModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
