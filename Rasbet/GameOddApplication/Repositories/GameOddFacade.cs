using AutoMapper;
using Domain;
using Domain.ResultDomain;
using DTO;
using DTO.GameOddDTO;
using DTO.GetGamesRespDTO;
using GameOddApplication.Exceptions;
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
        Game game = await gameRepository.GetGame(id);
        if(game.State != GameState.Finished)
        {
            await FinishGame(id, result, null);
        }
        return Unit.Value;
    }


    public async Task<Unit> UpdateGameOdd(ICollection<DTO.GetGamesRespDTO.GameDTO> games, string sportName)
    {
        foreach (DTO.GetGamesRespDTO.GameDTO game in games)
        {
            if (await gameRepository.HasGame(game.Id))
            {
                if (game.Completed == true)
                {
                    await FinishGame(game.Id, game.Scores);
                }
                else //O jogo ainda não acabou, atualizar as odds se necessário
                {
                    CollectiveGame g = await gameRepository.GetCollectiveGame(game.Id);
                    if(g.SpecialistId != null)
                        await betTypeRepository.UpdateBets(game.Bookmakers, g.AwayTeam, g.Id);
                }
            }
            else if (game.Completed == false)
            {
                Sport sport = await sportRepository.GetSport(sportName);
                Game g = await gameRepository.CreateCollectiveGame(sport, game.Id, game.CommenceTime, game.HomeTeam, game.AwayTeam);
                ICollection<BetType> betTypes = await betTypeRepository.CreateBets(game.Bookmakers, game.AwayTeam, g.Id);
            }
        }
        return Unit.Value;
    }

    public async Task<Unit> SuspendGame(int gameId, string specialistId)
    {
        Game game = await gameRepository.GetGame(gameId);
        return await gameRepository.ChangeGameState(game, specialistId, GameState.Suspended);
    }

    public async Task<Unit> ActivateGame(int gameId, string specialistId)
    {
        Game game = await gameRepository.GetGame(gameId);
        return await gameRepository.ChangeGameState(game, specialistId, GameState.Open);
    }

    public async Task<Unit> FinishGame(int id, string result, string specialistId)
    {
        ICollection<BetsOddsWonDTO> res = new List<BetsOddsWonDTO>();
        Game g = await gameRepository.GetGame(id);
        await gameRepository.ChangeGameState(g, specialistId, GameState.Finished);
        foreach (BetType betType in g.Bets)
        {
            if (specialistId != null)
                betType.SpecialistId = specialistId;
            betType.State = BetTypeState.FINISHED;
            res.Add(new BetsOddsWonDTO(betType.Id, betType.SetWinningOdd(result).Select(x => x.Id).ToList()));
            await gameOddContext.SaveChangesAsync();
        }
        await API.UpdateBets(res);
        return Unit.Value;
    }

    public async Task<Unit> FinishGame(string id, string result, string? specialistId)
    {
        ICollection<BetsOddsWonDTO> res = new List<BetsOddsWonDTO>();
        Game g = await gameRepository.GetGame(id);
        await gameRepository.ChangeGameState(g, specialistId, GameState.Finished);
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

    public async Task<ICollection<CollectiveGameDTO>> GetActiveGames()
    {
        ICollection<CollectiveGame> collectiveGames = await gameOddContext.Game.OfType<CollectiveGame>()
                                                                     .Where(g => g.State.Equals(GameState.Open))
                                                                     .Include(g => g.Sport)
                                                                     .Include(g => g.Bets)
                                                                     .ThenInclude(o => o.Odds)
                                                                     .ToListAsync();
        return mapper.Map<ICollection<CollectiveGameDTO>>(collectiveGames);
    }

    public async Task<double> GetOddValue(int oddId, int betTypeId)
    {
        BetType ?b = await gameOddContext.BetType.Where(b => b.Id == betTypeId)
                                                 .Include(b => b.Odds)
                                                 .FirstOrDefaultAsync();
        if (b == null)
            throw new BetTypeNotFoundException($"BetType {betTypeId} dont exist!");
        Odd ?d = b.Odds.Where(o => o.Id == oddId).FirstOrDefault();
        if (d == null)
            throw new OddNotFoundException($"Odd {oddId} dont exist!");
        return d.OddValue;
    }


    public async Task<Unit> ChangeOdds(string specialistId, int betTypeId, Dictionary<int, double> newOdds)
    {
        BetType bet = await betTypeRepository.GetBetType(betTypeId);
        bet.SpecialistId = specialistId;
        foreach(var item in newOdds)
        {
            Odd d = bet.Odds.FirstOrDefault(o => o.Id == item.Key);
            d.OddValue = item.Value;
        }
        await gameOddContext.SaveChangesAsync();
        return Unit.Value;
    }

    public async Task<GameInfoDTO> GetGameInfo(int gameId, bool detailed)
    {
        IQueryable<Game> game = gameOddContext.Game.Where(x => x.Id.Equals(gameId));
        if (detailed)
        {
            game.Include(x => x.Bets);
        }
        Game? g = await game.FirstOrDefaultAsync();
        if (g == null)
            throw new GameNotFoundException($"Game {gameId} not exists!");
        return mapper.Map<GameInfoDTO>(g);
    }

    public async Task<DTO.GameOddDTO.GameDTO> GetGame(int gameId)
    {
        Game g = await gameRepository.GetGame(gameId);
        return mapper.Map<DTO.GameOddDTO.GameDTO>(g);
    }

    public async Task<ICollection<SportDTO>> GetSports()
    {
        ICollection<Sport> sports = await gameOddContext.Sport.ToListAsync();
        return mapper.Map<ICollection<SportDTO>>(sports);
    }

    public async Task<ICollection<CollectiveGameDTO>> GetActiveAndSuspendedGames()
    {
        ICollection<Game> games = await gameRepository.GetActiveAndSuspendedGames();
        return mapper.Map<ICollection<CollectiveGameDTO>>(games);
    }
}
