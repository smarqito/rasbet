using Domain;
using Domain.UserDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly UserContext context;

    public WalletRepository(UserContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Get user wallet.
    /// </summary>
    /// <param name="userId">Id of the user whose wallet we want to retrieve.</param>
    /// <returns></returns>
    public async  Task<Wallet> Get(int userId)
    {
        Wallet wallet = context.Wallet.Where(w => w.User.Id.Equals(userId)).First();

        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");

        return wallet;
    }

    /// <summary>
    /// Deposit funds to a user.
    /// </summary>
    /// <param name="value"></param>
    public async Task<Wallet> DepositFunds(double value)
    {
        int userid = 0; // =.....
        Deposit d = new Deposit(userid,value);
        await context.Transactions.AddAsync(d);

        Wallet wallet = context.Wallet.Where(w => w.User.Id.Equals(userid)).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance += value;

        await context.SaveChangesAsync();
        return wallet;
    }

    /// <summary>
    /// Withdraw money from current user
    /// </summary>
    /// <param name="value"> Value to withdraw. </param>
    public async Task<Wallet> WithdrawFunds(double value)
    {
        int userid = 0; // =.....
        Withdraw w = new Withdraw(userid,value);
        await context.Transactions.AddAsync(w);
        
        Wallet wallet = context.Wallet.Where(w => w.User.Id.Equals(userid)).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance -= value;

        await context.SaveChangesAsync();
        return wallet;
    }
}
