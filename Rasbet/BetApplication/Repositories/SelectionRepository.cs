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

    public async Task<Selection> CreateSelection(double serverOdd, double chosenOdd, int betTypeId, int oddId ,int gameId )
    {
        try
        {
            //threshold 5%
            //Se a odd do servidor divergir da odd escolhida por 5% é lançado erro
            double threshold = serverOdd / chosenOdd;
            if (threshold >= 0.05 || threshold <= 0.95 )
            {
                throw new OddTooDiferentException("As odds do cliente e servidor) divergem em 5% ou mais!");
            }

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

    public Task<ICollection<Selection>> GetSelectionByGame(int game)
    {
        ICollection<Selection> selections = new List<Selection>();

        foreach(var selection in _context.Selections)
        {
            if(selection.GameId == game)
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
            if (selection.BetTypeId == bettype)
            {
                selections.Add(selection);
            }
        }

        if (selections.Count == 0)
            throw new NoSelectionsWithTypeException("Não existem seleções com apostas do tipo!");

        return (Task<ICollection<Selection>>)selections;
    }
}
