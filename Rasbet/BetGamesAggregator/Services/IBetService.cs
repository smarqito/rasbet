using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetGamesAggregator.Services
{
    public interface IBetService
    {
        public Task<ICollection<BetDTO>> GetUserBetsWon(string userId);
        public Task<ICollection<BetDTO>> GetUserBetsLost(string userId);
        public Task<ICollection<BetDTO>> GetUserBetsOpen(string userId);
        public Task<StatisticsDTO> GetStatisticsByGame(int gameId);
    }
}
