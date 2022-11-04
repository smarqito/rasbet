using Domain;
using Domain.ResultDomain;
using DTO.GetGamesRespDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Interfaces;

public interface IBetTypeRepository
{
    public Task<BetType> GetBetType(int id);
    public Task<ICollection<BetType>> CreateBets(ICollection<BookmakerDTO> bookmakers, string awayTeam);
    public Task<Unit> UpdateBets(ICollection<BetType> bets, ICollection<BookmakerDTO> bookmakers);
    public Task<Unit> ChangeBetTypeState(int id, BetTypeState state, string specialistId);
}
