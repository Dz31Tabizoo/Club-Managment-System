using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using WPF_APP.Models;
using CMS.Core.Interfaces;
using CMS.DTOs;


namespace WPF_APP.Services
{
    public class AuthService : IAuthenticationClientService
    {

        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7135/")
        };


        public UserDTO? CurrentUser { get; private set; }
        public event Action? OnAuthenticationStateChanged;



        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        public async Task<LoginResponseModel> LoginAsync(string username, string password)
        {
            var LoginData = new LoginRequestModel
            {
                Username = username,
                Password = password
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", LoginData);

                var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>();

                return result ?? new LoginResponseModel
                {
                    Success = false,
                    Message = "Erreur serveur."
                };
            }
            catch(Exception)
            {
                return new LoginResponseModel
                {
                    Success = false,
                    Message = "Erreur de connexion au serveur."
                };
            }

        }

        public void Login(UserDTO user)
        {
            CurrentUser = user;
            OnAuthenticationStateChanged?.Invoke();
        }
    }
}
