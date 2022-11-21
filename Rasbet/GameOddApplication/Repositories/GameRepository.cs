using Domain;
using Domain.ResultDomain;
using Domain.UserDomain;
using GameOddApplication.Exceptions;
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

    public async Task<Unit> ChangeGameState(Game g, GameState state)
    {
        return await ChangeGameState(g, null, state);
    }

    public async Task<Unit> ChangeGameState(Game g, string? specialistId, GameState state)
    {
        if (state != g.State)
        {
            g.State = state;
            if (specialistId != null)
                g.SpecialistId = specialistId;
            await gameOddContext.SaveChangesAsync();
        }
        else
        {
            throw new SameGameStateException("Game has already that state");
        }
        return Unit.Value;
    }

    public async Task<Game> CreateCollectiveGame(Sport sport, string idSync, DateTime date, string HomeTeam, string AwayTeam)
    {
        try
        {
            Game g = new CollectiveGame(HomeTeam, AwayTeam, idSync, date, sport);
            await gameOddContext.Game.AddAsync(g);
            await gameOddContext.SaveChangesAsync();
            return g;
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
            Game g = new IndividualGame(Players, idSync, date, sport);
            await gameOddContext.Game.AddAsync(g);
            await gameOddContext.SaveChangesAsync();
            return Unit.Value;
        }
        catch (Exception)
        {
            throw new Exception("Aconteceu um erro interno");
        }
    }

    public async Task<CollectiveGame> GetCollectiveGame(string idSync)
    {
        CollectiveGame g = await gameOddContext.Game.OfType<CollectiveGame>()
                                                    .FirstOrDefaultAsync(g => g.IdSync == idSync);
        if (g == null)
            throw new GameNotFoundException($"Game with id {idSync} don't exist!");
        return g;
    }

    public async Task<Game> GetGame(int id)
    {
        Game? g = await gameOddContext.Game.FirstOrDefaultAsync(g => g.Id== id);
        if (g == null)
            throw new GameNotFoundException($"Game with id {id} don't exist!");
        return g;
    }

    public async Task<Game> GetGame(string idSync)
    {
        Game ?g = await gameOddContext.Game.FirstOrDefaultAsync(g => g.IdSync == idSync);
        if (g == null)
            throw new GameNotFoundException($"Game with id {idSync} don't exist!");
        return g;
    }

    public async Task<bool> HasGame(string idSync)
    {
        return await gameOddContext.Game.Where(g => g.IdSync == idSync)
                                          .AnyAsync();
    }
}
