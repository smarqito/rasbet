using Domain;
using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;

namespace BetGamesAggregator.Services
{
    public class BetService : IBetService
    {
        private readonly HttpClient _client;

        public BetService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<StatisticsDTO> GetStatisticsByGame(ICollection<int> oddsId)
        {
            StringBuilder stringBuilder= new StringBuilder();
            for(int i = 0; i < oddsId.Count - 1; i++)
            {
                stringBuilder.Append($"oddIds={oddsId.ElementAt(i).ToString()}&");
            }
            stringBuilder.Append($"oddIds={oddsId.ElementAt(oddsId.Count - 1).ToString()}");
            var response = await _client.GetAsync($"/Selection/statistics?{stringBuilder}");
            return await response.Content.ReadFromJsonAsync<StatisticsDTO>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsClosed(string userId, DateTime start, DateTime end, string header)
        {
            _client.DefaultRequestHeaders.Add("Authorization", header);
            var response = await _client.GetAsync($"/Bet/closed?userId={userId}start={start.ToUniversalTime().ToString("o")}&end={end.ToUniversalTime().ToString("o")}");
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsOpen(string userId, DateTime start, DateTime end, string header)
        {
            _client.DefaultRequestHeaders.Add("Authorization", header);
            var response = await _client.GetAsync($"/Bet/open?userId={userId}&start={start.ToUniversalTime().ToString("o")}&end={end.ToUniversalTime().ToString("o")}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsWon(string userId, DateTime start, DateTime end, string header)
        {
            _client.DefaultRequestHeaders.Add("Authorization", header);
            var response = await _client.GetAsync($"/Bet/won?userId={userId}start={start.ToUniversalTime().ToString("o")}&end={end.ToUniversalTime().ToString("o")}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }
    }
}
