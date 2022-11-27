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

        public async Task<ICollection<BetDTO>> GetUserBetsLost(string userId)
        {
            var response = await _client.GetAsync($"/Bet/lost?userId={userId}");
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsOpen(string userId)
        {
            var response = await _client.GetAsync($"/Bet/open?userId={userId}");
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }

        public async Task<ICollection<BetDTO>> GetUserBetsWon(string userId)
        {
            var response = await _client.GetAsync($"/Bet/won?userId={userId}");
            return await response.Content.ReadFromJsonAsync<ICollection<BetDTO>>();
        }
    }
}
