using DTO.GameOddDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetGamesAggregator.Services
{
    public interface IGameOddService
    {
        public Task<ICollection<CollectiveGameDTO>> GetActivesGames();
        public Task<GameDTO> GetGame(int id);
    }
}
