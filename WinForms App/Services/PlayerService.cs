using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using WinForms_App.Models;
using Serilog;
using Serilog.Events;
using Serilog.Configuration;
using Serilog.Sinks.File;
using Newtonsoft.Json; // Ajoutez cette directive using

namespace WinForms_App
{
    public class PlayerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7135/api/Players/";
        public PlayerService()
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
                 .WriteTo.File("logs/desktop_app_log.txt", rollingInterval: RollingInterval.Day) // سيعمل الآن
                 .CreateLogger();

            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler);
        }


        public async Task<List<PlayerDTO>> GetAllPlayersAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl+ "playersWithDetails");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var players = JsonConvert.DeserializeObject<List<PlayerDTO>>(content);
                Log.Information("Fetched all players successfully.");
                return players;
            }
            Log.Warning("Api Failure. Status Code: {StatusCode}", response.StatusCode);
            return new List<PlayerDTO>();
        }
    }
}
