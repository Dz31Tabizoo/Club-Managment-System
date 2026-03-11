using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DTOs;

namespace Core.Interfaces
{
    internal interface IAuthenticationService
    {
        UserDTO? CurrentUser { get; }
        bool IsLoggedIn { get; }


        event Action? OnAuthenticationStateChanged;

        void Login(UserDTO user);
        void Logout();
    }
}
