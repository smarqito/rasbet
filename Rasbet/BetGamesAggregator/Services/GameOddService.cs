using Domain;
using DTO.BetDTO;
using DTO.GameOddDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetGamesAggregator.Services
{
    public class GameOddService : IGameOddService
    {
        private readonly HttpClient _client;

        public GameOddService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<GameDTO> GetGame(int ids)
        {
            var response = await _client.GetAsync("/GameOdd");
            return await response.Content.ReadFromJsonAsync<GameDTO>();
        }
    }
}
