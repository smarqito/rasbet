using BetApplication.Errors;
using BetApplication.Interfaces;
using BetPersistence;
using Domain;

namespace BetApplication.Repositories;

public class TransactionRepository : ITransactionRepository
{
    //private readonly BetContext _context;

    //public TransactionRepository(BetContext context)
    //{
    //    _context = context;
    //}

    //public async Task<Transaction> MakeDeposit(AppUser user, double balance)
    //{
    //    try
    //    {
    //        Deposit d = new Deposit(balance);

    //        user.Wallet.Transactions.Add(d);

    //        //Adicionar balanço ao utilizador

    //        await _context.Transactions.AddAsync(d);
    //        await _context.SaveChangesAsync();

    //        return d;
    //    }
    //    catch(Exception)
    //    {
    //        throw new Exception("Ocorreu um erro interno!");
    //    }
    //}

    //public async Task<Transaction> WithdrawBalance(AppUser user, double balance)
    //{
    //    try
    //    {
    //        if (user.Wallet.Balance > balance)
    //        {
    //            Withdraw w = new Withdraw(balance);

    //            user.Wallet.Transactions.Add(w);

    //            //Adicionar balanço ao utilizador

    //            await _context.Transactions.AddAsync(w);
    //            await _context.SaveChangesAsync();

    //            return w;
    //        }

    //        throw new UserBalanceTooLowException("O utilizador não tem saldo suficiente!");
    //    }
    //    catch (Exception)
    //    {
    //        throw new Exception("Ocorreu um erro interno!");
    //    }
    //}
    public Task<Transaction> MakeDeposit(AppUser user, double balance)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> WithdrawBalance(AppUser user, double balance)
    {
        throw new NotImplementedException();
    }
}
