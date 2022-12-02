using DTO.BetDTO;
using Microsoft.AspNetCore.Mvc;

namespace BetGamesAggregator.Services
{
    public interface IBetService
    {
        public Task<ICollection<BetDTO>> GetUserBetsWon(string userId, DateTime start, DateTime end, string header);
        public Task<ICollection<BetDTO>> GetUserBetsClosed(string userId, DateTime start, DateTime end, string header);
        public Task<ICollection<BetDTO>> GetUserBetsOpen(string userId, DateTime start, DateTime end, string header);
        public Task<StatisticsDTO> GetStatisticsByGame(ICollection<int> oddsId);
    }
}
