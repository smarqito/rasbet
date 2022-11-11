using BetApplication.Errors;
using BetApplication.Interfaces;
using BetPersistence;
using Domain;
using Domain.ResultDomain;
using Microsoft.EntityFrameworkCore;

namespace BetApplication.Repositories;

public class SelectionRepository : ISelectionRepository
{
    private readonly BetContext _context;

    public SelectionRepository(BetContext context)
    {
        _context = context;
    }

    public async Task<Selection> GetSelectionById(int id)
    {
        Selection ?s;
        try
        {
            s = await _context.Selections.FindAsync(id);
        }
        catch(Exception)
        {
            throw new Exception("Aconteceu um erro interno!");
        }

        if (s != null)
            return s;
        
        throw new SelectionNotExistException("O id não corresponde a uma seleção existente!");
    }

    public async Task<Selection> CreateSelection(double serverOdd, double chosenOdd, int betTypeId, int oddId ,int gameId )
    {
        //threshold 5%
        //Se a odd do servidor divergir da odd escolhida por 5% é lançado erro
        double threshold = serverOdd / chosenOdd;
        if (threshold >= 0.05 && threshold <= 0.95 )
        {
            throw new OddTooDiferentException("As odds do cliente e servidor) divergem em 5% ou mais!");
        }

        try
        {

            Selection newS = new Selection(oddId, chosenOdd, betTypeId, gameId);
            await _context.Selections.AddAsync(newS);
            await _context.SaveChangesAsync();
            return newS;
        }
        catch(Exception)
        {
            throw new Exception("Aconteceu um erro interno!");
        }
    }

    public async Task<ICollection<Selection>> GetSelectionByGame(int game)
    {
        ICollection<Selection> selections = new List<Selection>();
        var server_selections = await _context.Selections.ToListAsync();

        foreach (var selection in server_selections)
        {
            if(selection.GameId == game)
            {
                selections.Add(selection);
            }
        }

        if (selections.Count == 0)
            throw new NoSelectionsInGameException("Não existem seleções no jogo!");

        return selections;
    }

    public async Task<ICollection<Selection>> GetSelectionByType(int bettype)
    {
        ICollection<Selection> server_selections = await _context.Selections.Where(x => x.BetTypeId ==bettype).ToListAsync();

        if (server_selections.Count == 0)
            throw new NoSelectionsWithTypeException("Não existem seleções com apostas do tipo!");

        return server_selections;
    }
}
