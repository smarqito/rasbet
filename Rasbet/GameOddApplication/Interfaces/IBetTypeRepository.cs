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
    public Task<ICollection<BetType>> CreateBets(ICollection<BookmakerDTO> bookmakers, string awayTeam, int id);
    public Task<ICollection<Odd>> UpdateBets(ICollection<BookmakerDTO> bookmakers, string awayTeam, int gameId);
    public Task<Unit> ChangeBetTypeState(int id, BetTypeState state, string specialistId);
}
