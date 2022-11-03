using Domain;
using Domain.ResultDomain;
using DTO.GetGamesRespDTO;
using GameOddApplication.Interfaces;
using GameOddPersistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Repositories;

public class GameOddFacade : IGameOddFacade
{
    private readonly GameOddContext gameOddContext;
    private readonly IGameRepository gameRepository;
    private readonly IBetTypeRepository betTypeRepository;
    private readonly ISportRepository sportRepository;

    public GameOddFacade(GameOddContext gameOddContext, IGameRepository gameRepository, IBetTypeRepository betTypeRepository, ISportRepository sportRepository)
    {
        this.gameOddContext = gameOddContext;
        this.gameRepository = gameRepository;
        this.betTypeRepository = betTypeRepository;
        this.sportRepository = sportRepository;
    }

    public async Task<Unit> FinishGame(string id, string result)
    {
        Game g = await gameRepository.GetGame(id);
        foreach(BetType betType in g.Bets)
        {
            betType.SetWinningOdd(result);
        }
        //enviar info das ods vencedoras à BetAPI
        throw new NotImplementedException();
    }

    public async Task<Unit> UpdateGameOdd(ICollection<GameDTO> games, string sportName)
    {
        foreach (GameDTO game in games)
        {
            if (await gameRepository.HasGame(game.Id))
            {
                if (game.Completed == true)
                {
                    await FinishGame(game.Id, game.Scores);
                }
                else //O jogo ainda não acabou, atualizar as odds se necessário
                {
                    Game g = await gameRepository.GetGame(game.Id);
                    await betTypeRepository.UpdateBets(g.Bets, game.Bookmakers);
                }
            }
            else if (game.Completed == false)
            {
                ICollection<BetType> betTypes = await betTypeRepository.CreateBets(game.Bookmakers, game.AwayTeam);
                Sport sport = await sportRepository.GetSport(sportName);
                await gameRepository.CreateCollectiveGame(sport, game.Id, game.CommenceTime, game.HomeTeam, game.AwayTeam, betTypes);
            }
        }
        return Unit.Value;
    }
}
