using Domain.ResultDomain;
using DTO.GetGamesRespDTO;
using GameOddApplication.Exceptions;
using GameOddApplication.Interfaces;
using GameOddPersistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace GameOddApplication.Repositories;

public class BetTypeRepository : IBetTypeRepository
{
    private readonly GameOddContext gameOddContext;

    public BetTypeRepository(GameOddContext gameOddContext)
    {
        this.gameOddContext = gameOddContext;
    }

    public async Task<Unit> ChangeBetTypeState(int id, BetTypeState state, string specialistId)
    {
        BetType bet = await GetBetType(id);
        if (state != bet.State)
        {
            bet.State = state;
            bet.SpecialistId = specialistId;
            await gameOddContext.SaveChangesAsync();
        }
        return Unit.Value;
    }

    public async Task<ICollection<BetType>> CreateBets(ICollection<BookmakerDTO> bookmakers, string awayTeam, int gameId)
    {
        List<BetType> betTypes = CalculateBets(bookmakers, awayTeam, gameId);
        await gameOddContext.BetType.AddRangeAsync(betTypes);
        await gameOddContext.SaveChangesAsync();
        return betTypes;
    }


    public async Task<BetType> GetBetType(int id)
    {
        BetType? bet = await gameOddContext.BetType.FirstOrDefaultAsync(g => g.Id == id);
        if (bet == null)
            throw new BetTypeNotFoundException($"BetType with id {id} don't exist!");
        return bet;
    }

    public async Task<ICollection<BetType>> GetBetTypesByGameId(int gameId)
    {
        return await gameOddContext.BetType.Where(x => x.GameId == gameId).ToListAsync();
    }

    public async Task<ICollection<BetType>> GetBetTypesByGameIdSync(string gameId)
    {
        return await gameOddContext.BetType.Where(x => x.GameId.Equals(gameId)).ToListAsync();
    }


    public async Task<Unit> UpdateBets(ICollection<BookmakerDTO> bookmakers, string awayTeam, int gameId)
    {
        List<BetType> betTypes = new();
        DateTime lastUpdate = bookmakers.Select(x => x.LastUpdate).Max();
        IEnumerable<IGrouping<string, MarketDTO>> markets = bookmakers.SelectMany(x => x.Markets)
                                                                      .Where(x => !x.Key.Equals("h2h_lay"))
                                                                      .GroupBy(x => x.Key);
        foreach (IGrouping<string, MarketDTO> market_g in markets)
        {
            Dictionary<string, Odd> ods = new();
            H2h h2h = new(awayTeam, lastUpdate, gameId);
            betTypes.Add(h2h);
            BetType bet = await gameOddContext.BetType.FromSqlRaw($"SELECT * FROM dbo.BetType WHERE LOWER(Discriminator) LIKE LOWER('{market_g.Key}') AND GameId = {gameId}")
                                                      .FirstOrDefaultAsync();
            if (lastUpdate > bet.LastUpdate && bet.SpecialistId != null)
            {

                foreach (MarketDTO market in market_g)
                {
                    foreach (OutcomesDTO outcome in market.Outcomes)
                    {
                        if (!ods.ContainsKey(outcome.Name))
                        {
                            Odd d = bet.Odds.FirstOrDefault(x => x.Name == outcome.Name);
                            d.ResetOdd();
                            ods.Add(outcome.Name, d);
                        }
                        Odd odd = ods[outcome.Name];
                        odd.UpdateOdd(outcome.Price);
                    }
                }
                bet.LastUpdate = lastUpdate;
            }
        }
        await gameOddContext.SaveChangesAsync();
        return Unit.Value;
    }

    public List<BetType> CalculateBets(ICollection<BookmakerDTO> bookmakers, string awayTeam, int gameId)
    {
        List<BetType> betTypes = new();
        DateTime lastUpdate = bookmakers.Select(x => x.LastUpdate).Max();
        IEnumerable<IGrouping<string, MarketDTO>> markets = bookmakers.SelectMany(x => x.Markets)
                                                                      .Where(x => !x.Key.Equals("h2h_lay"))
                                                                      .GroupBy(x => x.Key);
        foreach (IGrouping<string, MarketDTO> market_g in markets)
        {

            Dictionary<string, Odd> ods = new();
            H2h h2h = new(awayTeam, lastUpdate, gameId);
            betTypes.Add(h2h);
            foreach (MarketDTO market in market_g)
            {
                foreach (OutcomesDTO outcome in market.Outcomes)
                {
                    if (!ods.ContainsKey(outcome.Name))
                    {
                        ods.Add(outcome.Name, new Odd(outcome.Name));
                    }
                    Odd odd = ods[outcome.Name];
                    odd.UpdateOdd(outcome.Price);
                }
            }
            ((List<Odd>)h2h.Odds).AddRange(ods.Values);
        }
        return betTypes;
    }
}
