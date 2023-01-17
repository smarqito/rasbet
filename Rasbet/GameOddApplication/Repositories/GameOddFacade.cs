using AutoMapper;
using Domain;
using Domain.ResultDomain;
using Domain.UserDomain;
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
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
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
            await NotifyUsers((CollectiveGame)game);
        }
        return Unit.Value;
    }

    public async Task NotifyUsers(CollectiveGame g, ICollection<Odd> newOdds)
    {
        ChangeGameDTO changeGameDTO = new ChangeGameDTO(g.GetFolowersId(), g.HomeTeam, g.AwayTeam, g.StartTime, mapper.Map<ICollection<OddDTO>>(newOdds));
        await API.NotifyFollowers(changeGameDTO);
    }

    public async Task NotifyUsers(CollectiveGame g)
    {
        ChangeGameDTO changeGameDTO = new ChangeGameDTO(g.GetFolowersId(), g.State.ToString(), g.HomeTeam, g.AwayTeam, g.StartTime);
        await API.NotifyFollowers(changeGameDTO);
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
                    {
                        ICollection<Odd> newOdds = await betTypeRepository.UpdateBets(game.Bookmakers, g.AwayTeam, g.Id);
                        await NotifyUsers(g, newOdds);
                    }
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
        await gameRepository.ChangeGameState(game, specialistId, GameState.Suspended);
        await NotifyUsers((CollectiveGame)game);
        return Unit.Value;
    }

    public async Task<Unit> ActivateGame(int gameId, string specialistId)
    {
        Game game = await gameRepository.GetGame(gameId);
        await gameRepository.ChangeGameState(game, specialistId, GameState.Open);
        await NotifyUsers((CollectiveGame)game);
        return Unit.Value;
    }

    public async Task<Unit> FinishGame(int gameId, string result, string specialistId)
    {
        string id = (await gameRepository.GetGame(gameId)).IdSync;
        return await FinishGame(id, result, specialistId);
    }

    public async Task<Unit> FinishGame(string gameId, string result, string specialistId)
    {
        ICollection<BetsOddsWonDTO> res = new List<BetsOddsWonDTO>();
        Game g = await gameRepository.GetGame(gameId);
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
        await NotifyUsers((CollectiveGame)g);
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


    public async Task<Unit> ChangeOdds(string specialistId, int betTypeId, ICollection<NewODD> newOdds)
    {
        BetType bet = await betTypeRepository.GetBetType(betTypeId);
        ICollection<Odd> odds = new List<Odd>();
        bet.SpecialistId = specialistId;
        foreach(var item in newOdds)
        {
            Odd d = bet.Odds.FirstOrDefault(o => o.Id == item.OddId);
            d.OddValue = item.OddValue;
            odds.Add(d);
        }
        await gameOddContext.SaveChangesAsync();
        CollectiveGame? g = await gameOddContext.Game.OfType<CollectiveGame>().Where(g => g.Bets.Any(b => b.Id == betTypeId)).FirstOrDefaultAsync();
        if (g != null)
        {
            await NotifyUsers(g, odds);
        }

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

    public async Task FollowGame(string userId, int gameId)
    {
        Follower f = new Follower(userId);
        if (gameOddContext.Follower.Where(f => f.UserId.Equals(userId) && f.Game.Id == gameId).Count() == 0)
        {
            (await gameRepository.GetGame(gameId)).FollowersIds.Add(f);
            await gameOddContext.SaveChangesAsync();
        }
    }

    public async Task UnfollowGame(string userId, int gameId)
    {
        Game g = await gameRepository.GetGame(gameId);
        Follower? f = g.FollowersIds.Where(f => f.UserId.Equals(userId)).FirstOrDefault();
        if (f != null)
        {
            g.FollowersIds.Remove(f);
            await gameOddContext.SaveChangesAsync();
        }
    }

    public async Task<ICollection<int>> GetGamesFollowed(string userId)
    {
        ICollection<Game> games = await gameOddContext.Game.Where(g => g.State.Equals(GameState.Open) && g.FollowersIds.Any(f => f.UserId.Equals(userId)))
                                        .ToListAsync();
        return games.Select(g => g.Id).ToList();
    }

}
