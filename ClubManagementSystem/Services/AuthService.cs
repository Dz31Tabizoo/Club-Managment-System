
using ClubManagementSystem.Core;
using ClubManagementSystem.Models;
using CMS.Core.Interfaces;
using CMS.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


namespace ClubManagementSystem.Services
{
    public class AuthService : IAuthenticationClientService
    {

        private readonly HttpClient _httpClient;
        

        public bool IsLoggedIn { get; private set; }
        public UserModel? CurrentUser { get; private set; }
        public event Action? OnAuthenticationStateChanged;



        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        

        public async Task<LoginResponseModel> LoginAsync(string username, string password)
        {
            var loginData = new LoginRequestModel
            {
                Username = username,
                Password = password
            };

            

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginData).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>(options).ConfigureAwait(false);

                    return result ?? new LoginResponseModel
                    {
                        Success = false,
                        Message = "Reponse vide du serveur."
                    };
                }
                
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new LoginResponseModel { Success = false, Message = "Identifiants incorrects." };
                }

                return new LoginResponseModel { Success = false, Message = $"Erreur serveur : {response.StatusCode}" };
            }
            catch (HttpRequestException)
            {
                return new LoginResponseModel { Success = false, Message = "Impossible de contacter le serveur API." };
            }
            catch (Exception ex)
            {
                return new LoginResponseModel { Success = false, Message =  $"Une erreur inattendue est survenue: {ex.Message}" };
            }
        }

        public void Logout()
        {
            CurrentUser = null;

            IsLoggedIn = false;

            UserSession.Token = null;
            UserSession.DisplayName = null;

            OnAuthenticationStateChanged?.Invoke();
        }

        public void Login(UserModel user)
        {
            CurrentUser = user;
            IsLoggedIn = true;
            OnAuthenticationStateChanged?.Invoke();
        }
    }
}
