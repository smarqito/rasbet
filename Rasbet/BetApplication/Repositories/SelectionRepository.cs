using BetApplication.Errors;
using BetApplication.Interfaces;
using BetPersistence;
using Domain;
using Domain.ResultDomain;

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
        try
        {
            var s = await _context.Selections.FindAsync(id);

            if (s != null)
                return s;

            else
                throw new SelectionNotExistException("O id não corresponde a uma seleção existente!");
        }
        catch(Exception)
        {
            throw new Exception("Aconteceu um erro interno!");
        }
    }

    public async Task<Selection> CreateSelection(BetType bettype, Odd chosenOdd)
    {
        try
        {
            Selection newS = new Selection(chosenOdd.Id, chosenOdd.OddValue, bettype);
            await _context.Selections.AddAsync(newS);
            await _context.SaveChangesAsync();
            return newS;
        }
        catch(Exception)
        {
            throw new Exception("Aconteceu um erro interno!");
        }
    }

    public Task<ICollection<Selection>> GetSelectionByGame(int game)
    {
        ICollection<Selection> selections = new List<Selection>();

        foreach(var selection in _context.Selections)
        {
            if(selection.Result.Game.Id == game)
            {
                selections.Add(selection);
            }
        }

        if (selections.Count == 0)
            throw new NoSelectionsInGameException("Não existem seleções no jogo!");

        return (Task<ICollection<Selection>>) selections;
    }

    public Task<ICollection<Selection>> GetSelectionByType(int bettype)
    {
        ICollection<Selection> selections = new List<Selection>();

        foreach (var selection in _context.Selections)
        {
            if (selection.Result.Id == bettype)
            {
                selections.Add(selection);
            }
        }

        if (selections.Count == 0)
            throw new NoSelectionsWithTypeException("Não existem seleções com apostas do tipo!");

        return (Task<ICollection<Selection>>)selections;
    }
}
