using AutoMapper;
using Domain;
using Domain.ResultDomain;
using DTO;
using DTO.GameOddDTO;
using DTO.GetGamesRespDTO;
using GameOddApplication.Interfaces;
using GameOddPersistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    readonly IMapper mapper;
    private APIService API = new();

    public GameOddFacade(GameOddContext gameOddContext, IGameRepository gameRepository, IBetTypeRepository betTypeRepository, ISportRepository sportRepository, IMapper mapper)
    {
        this.gameOddContext = gameOddContext;
        this.gameRepository = gameRepository;
        this.betTypeRepository = betTypeRepository;
        this.sportRepository = sportRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> FinishGame(string id, string result)
    {
        await FinishGame(id, result, null);
        return Unit.Value;
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

    public async Task<Unit> SuspendGame(string gameId, string specialistId)
    {
        return await gameRepository.ChangeGameState(gameId, specialistId, GameState.Suspended);
    }

    public async Task<Unit> FinishGame(string id, string result, string? specialistId)
    {
        ICollection<BetsOddsWonDTO> res = new List<BetsOddsWonDTO>();
        Game g = await gameRepository.GetGame(id);
        g.State = GameState.Finished;
        foreach (BetType betType in g.Bets)
        {
            if (specialistId != null)
                betType.SpecialistId = specialistId;
            betType.State = BetTypeState.FINISHED;
            res.Add(new BetsOddsWonDTO(betType.Id , betType.SetWinningOdd(result).Select(x => x.Id).ToList()));
            await gameOddContext.SaveChangesAsync();
        }
        await API.UpdateBets(res);
        return Unit.Value;
    }

    public async Task<ICollection<ActiveGameDTO>> GetActiveGames()
    {
        ICollection<Game> games = await gameOddContext.Game.Where(g => g.State.Equals(GameState.Open))
                                                     .Include(g => g.Sport)
                                                     .Include(g => g.Bets).ThenInclude(o => o.Odds)
                                                     .ToListAsync();
        return mapper.Map<ICollection<ActiveGameDTO>>(games);
    }

    public async Task<double> GetOddValue(int oddId, int betTypeId)
    {
        BetType ?b = await gameOddContext.BetType.FirstOrDefaultAsync(b => b.Id == betTypeId);
        if (b == null)
            throw new Exception();
        Odd ?d = b.Odds.FirstOrDefault(o => o.Id == oddId);
        if (d == null)
            throw new Exception();
        return d.OddValue;
    }
}
