using CMS.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using ClubManagementSystem.Models;

namespace ClubManagementSystem.Services
{
    public interface IAuthenticationClientService
    {
        Task<LoginResponseModel> LoginAsync(string username, string password);
        void Login(UserModel user); 
        
        // Pour notifier l'UI
        event Action OnAuthenticationStateChanged;
        UserModel? CurrentUser { get; }
    }
}

