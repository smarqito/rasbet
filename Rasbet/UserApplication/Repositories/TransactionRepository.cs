using Domain;
using UserApplication.Errors;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly UserContext _context;

    public TransactionRepository(UserContext context)
    {
        _context = context;
    }

    public async Task<Transaction> MakeDeposit(AppUser user, double balance)
    {
        try
        {
            Deposit d = new Deposit(balance);

            user.Wallet.Transactions.Add(d);

            //Adicionar balanço ao utilizador

            await _context.Transaction.AddAsync(d);
            await _context.SaveChangesAsync();

            return d;
        }
        catch (Exception)
        {
            throw new Exception("Ocorreu um erro interno!");
        }
    }

    public async Task<Transaction> WithdrawBalance(AppUser user, double balance)
    {
        try
        {
            if (user.Wallet.Balance > balance)
            {
                Withdraw w = new Withdraw(balance);

                user.Wallet.Transactions.Add(w);

                //Retirar balanço ao utilizador

                await _context.Transaction.AddAsync(w);
                await _context.SaveChangesAsync();

                return w;
            }

            throw new UserBalanceTooLowException("O utilizador não tem saldo suficiente!");
        }
        catch (Exception)
        {
            throw new Exception("Ocorreu um erro interno!");
        }
    }

}
