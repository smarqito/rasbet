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

        public async Task<ICollection<GameDTO>> GetActivesGames()
        {
            var response = await _client.GetAsync("/GameOdd/activeGames");
            return await response.Content.ReadFromJsonAsync<ICollection<GameDTO>>();
        }

        public async Task<GameDTO> GetGame(int ids)
        {
            var response = await _client.GetAsync("/GameOdd");
            return await response.Content.ReadFromJsonAsync<GameDTO>();
        }
    }
}
