using ClubManagementSystem.Core;
using ClubManagementSystem.Models;
using Serilog;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClubManagementSystem.Services
{
    public class MembersService : IMemberService
    {
        private readonly HttpClient _httpClient;       

        public MembersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static readonly JsonSerializerOptions MembersJsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<PersonModel>> GetAllMembersasync()
        {
            try
            {

                if (!string.IsNullOrEmpty(UserSession.Token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSession.Token);
                }
                var members = await _httpClient.GetFromJsonAsync<List<PersonModel>>("api/Members", MembersJsonOptions);
                
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

        public async Task<bool> SaveMemberAsync(PersonModel member)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Players/add", member, MembersJsonOptions);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException httpex)
            {
                Log.Error("Saving member failed: " + httpex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected Error: " + ex.Message);
                return false;
            }
        }
    }
}
