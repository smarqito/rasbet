using Domain;
using Domain.ResultDomain;
using DTO.GetGamesRespDTO;
using GameOddApplication.Exceptions;
using GameOddApplication.Interfaces;
using GameOddPersistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

    public async Task<ICollection<BetType>> CreateBets(ICollection<BookmakerDTO> bookmakers, string awayTeam)
    {
        List<BetType> betTypes = CalculateOdds(bookmakers, awayTeam);
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

    public async Task<Unit> UpdateBets(ICollection<BetType> bets, ICollection<BookmakerDTO> bookmakers)
    {
        DateTime lastUpdate = bookmakers.Select(x => x.LastUpdate).Max();
        foreach(BetType betType in bets)
        {
            if(lastUpdate > betType.LastUpdate)
            {
                H2h h2h = (H2h)betType;
                h2h.Odds = (ICollection<Odd>)CalculateOdds(bookmakers, h2h.AwayTeam);
                betType.LastUpdate = lastUpdate;
                await gameOddContext.SaveChangesAsync();
            }
        }
        return Unit.Value;
    }

    public List<BetType> CalculateOdds(ICollection<BookmakerDTO> bookmakers, string awayTeam)
    {
        List<BetType> betTypes = new();
        DateTime lastUpdate = bookmakers.Select(x => x.LastUpdate).Max();
        IEnumerable<IGrouping<string, MarketDTO>> markets = bookmakers.SelectMany(x => x.Markets).GroupBy(x => x.Key);
        foreach (IGrouping<string, MarketDTO> market_g in markets)
        {

            Dictionary<string, Odd> ods = new();
            H2h h2h = new(awayTeam, lastUpdate);
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
