using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManagementSystem.Core
{
    public static class UserSession
    {
        public static int? UserId { get;  set; }
        public static string? DisplayName { get; set; }
        public static string? Token { get; set; }
        public static int? Role { get; set; }

        public static void Logout()
        {
                UserId = null;
                DisplayName = null;
                Token = null;
                Role = null;
        }

    }
}
