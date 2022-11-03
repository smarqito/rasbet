using Domain;

namespace BetApplication.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> MakeDeposit(AppUser user, double balance);

    Task<Transaction> WithdrawBalance(AppUser user, double balance);
}
