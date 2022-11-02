using DTO.GetGamesRespDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Interfaces;

public interface IGameOddFacade
{
    public Task<Unit> UpdateGameOdd(ICollection<GameDTO> games, string sportName);
    public Task<Unit> FinishGame(string id, string result);
}
