﻿using AutoMapper;
using Domain;
using Domain.ResultDomain;
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

    public async Task<Unit> SuspendGame(string specialistId)
    {
        return await gameRepository.ChangeGameState(specialistId, GameState.Suspended);
    }

    public async Task<Unit> FinishGame(string id, string result, string specialistId)
    {
        Game g = await gameRepository.GetGame(id);
        await gameRepository.ChangeGameState(id, GameState.Finished);
        foreach (BetType betType in g.Bets)
        {
            if (specialistId != null)
                betType.SpecialistId = specialistId;
            betType.State = BetTypeState.FINISHED;
            betType.SetWinningOdd(result);
            await gameOddContext.SaveChangesAsync();
        }
        throw new NotImplementedException();
    }

    public async Task<ICollection<ActiveGameDTO>> GetActiveGames()
    {
        ICollection<Game> games = await gameOddContext.Game.Where(g => g.State.Equals(GameState.Open))
                                                     .Include(g => g.Sport)
                                                     .Include(g => g.Bets).ThenInclude(o => o.Odds)
                                                     .ToListAsync();
        return mapper.Map<ICollection<ActiveGameDTO>>(games);
    }
}
