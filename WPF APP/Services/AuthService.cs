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
        


        public UserModel? CurrentUser { get; private set; }
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

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>(options);

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
                return new LoginResponseModel { Success = false, Message = "Une erreur inattendue est survenue." };
            }
        }

        

        public void Login(UserModel user)
        {
            CurrentUser = user;
            OnAuthenticationStateChanged?.Invoke();
        }
    }
}
