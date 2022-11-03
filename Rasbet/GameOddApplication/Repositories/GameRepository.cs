using Domain;
using Domain.ResultDomain;
using Domain.UserDomain;
using GameOddApplication.Interfaces;
using GameOddPersistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameOddApplication.Repositories;

public class GameRepository : IGameRepository
{
    private readonly GameOddContext gameOddContext;

    public GameRepository(GameOddContext gameOddContext)
    {
        this.gameOddContext = gameOddContext;
    }

    public async Task<Unit> ChangeGameState(string idSync, GameState state, string specialistId)
    {
        Game g = await GetGame(idSync);
        if (state != g.State)
        {
            g.State = state;
            g.SpecialistId = specialistId;
            await gameOddContext.SaveChangesAsync();
        }
        return Unit.Value;
    }

    public async Task<Unit> ChangeGameState(string idSync, GameState state)
    {
        Game g = await GetGame(idSync);
        if (state != g.State)
        {
            g.State = state;
            await gameOddContext.SaveChangesAsync();
        }
        return Unit.Value;
    }

    public async Task<Unit> ChangeGameState(string gameId, string specialistId, GameState state)
    {
        Game g = await GetGame(gameId);
        if (state != g.State)
        {
            g.State = state;
            g.SpecialistId = specialistId;
            await gameOddContext.SaveChangesAsync();
        }
        return Unit.Value;
    }

    public async Task<Unit> CreateCollectiveGame(Sport sport, string idSync, DateTime date, string HomeTeam, string AwayTeam, ICollection<BetType> bets)
    {
        try
        {
            Game g = new CollectiveGame(HomeTeam, AwayTeam, idSync, date, sport, bets);
            await gameOddContext.Game.AddAsync(g);
            await gameOddContext.SaveChangesAsync();
            return Unit.Value;
        }
        catch (Exception)
        {
            throw new Exception("Aconteceu um erro interno");
        }

    }

    public async Task<Unit> CreateIndividuallGame(Sport sport, string idSync, DateTime date, ICollection<string> Players, ICollection<BetType> bets)
    {
        try
        {
            Game g = new IndividualGame(Players, idSync, date, sport, bets);
            await gameOddContext.Game.AddAsync(g);
            await gameOddContext.SaveChangesAsync();
            return Unit.Value;
        }
        catch (Exception)
        {
            throw new Exception("Aconteceu um erro interno");
        }
    }

    public async Task<Game> GetGame(string idSync)
    {
        Game g = await gameOddContext.Game.Where(g => g.IdSync == idSync)
                                          .FirstOrDefaultAsync();
        if (g == null)
            throw new Exception();
        return g;
    }

    public async Task<bool> HasGame(string idSync)
    {
        return await gameOddContext.Game.Where(g => g.IdSync == idSync)
                                          .AnyAsync();
    }
}
