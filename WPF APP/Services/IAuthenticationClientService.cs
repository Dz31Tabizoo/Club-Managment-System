using CMS.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_APP.Models;

namespace WPF_APP.Services
{
    public interface IAuthenticationClientService
    {
        Task<LoginResponseModel> LoginAsync(string username, string password);
        void Login(UserDTO user); 
        
        // Pour notifier l'UI
        event Action OnAuthenticationStateChanged;
        UserDTO? CurrentUser { get; }
    }
}

