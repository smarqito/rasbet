using Domain;
using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetGamesAggregator.Services
{
    public class BetService : IBetService
    {
        private readonly HttpClient _client;

        public BetService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<StatisticsDTO> GetStatisticsByGame(int gameId)
        {
            var response = await _client.GetAsync($"/Selection/statistics?gameId={gameId}");
            return await response.Content.ReadFromJsonAsync<StatisticsDTO>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsClosed(string userId, DateTime start, DateTime end)
        {
            var response = await _client.GetAsync($"/Bet/closed?userId={userId}&start={start}&end={end}");
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsOpen(string userId, DateTime start, DateTime end)
        {
            var response = await _client.GetAsync($"/Bet/open?userId={userId}&start={start}&end={end}");
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsWon(string userId, DateTime start, DateTime end)
        {
            var response = await _client.GetAsync($"/Bet/won?userId={userId}&start={start}&end={end}");
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }
    }
}
