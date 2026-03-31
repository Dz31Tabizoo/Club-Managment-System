using ClubManagementSystem.Core;
using ClubManagementSystem.Models;
using CMS.Core.Interfaces;
using CMS.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ClubManagementSystem.Services
{
    public class MembersService : IMemberService
    {
        private readonly HttpClient _httpClient;


        

        public MembersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PersonModel>> GetAllMembersasync()
        {
            try
            {

                if (!string.IsNullOrEmpty(UserSession.Token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSession.Token);
                }
                var members = await _httpClient.GetFromJsonAsync<List<PersonModel>>("api/Members");
                
                return members ?? new List<PersonModel>();
                
            }
            catch(HttpRequestException httpex)
            {
                Log.Error("Fetsing all Members failed: " + httpex.Message);
                return new List<PersonModel>();
            }
            catch (Exception ex) 
            {
                Log.Error("Unnexpected Error: " + ex.Message);
                return new List<PersonModel>();
            }
            finally
            {
            }
            
        }



    }
}
