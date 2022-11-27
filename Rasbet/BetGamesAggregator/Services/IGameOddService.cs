using DTO.GameOddDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetGamesAggregator.Services
{
    public interface IGameOddService
    {
        public Task<ICollection<GameInfoDTO>> GetGames(ICollection<int> ids);
    }
}
