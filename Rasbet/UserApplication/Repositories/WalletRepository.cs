using Domain;
using DTO;
using DTO.BetDTO;
using DTO.UserDTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using UserApplication.Errors;
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
    public async Task<WalletDTO> Get(string userId)
    {
        Wallet? wallet = await context.Wallet.Where(w => w.UserId.Equals(userId)).FirstOrDefaultAsync();

        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");

        WalletDTO dto = new WalletDTO(wallet.UserId, wallet.Balance);

        return dto;
    }

    public async Task<Wallet> GetWallet(string userId)
    {
        Wallet? wallet = await context.Wallet.Where(w => w.UserId.Equals(userId)).FirstOrDefaultAsync();

        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");

        return wallet;
    }

    /// <summary>
    /// Deposit funds to a user.
    /// </summary>
    /// <param name="value"></param>
    public async Task DepositFunds(string userId, double value)
    {
        Wallet? wallet = await context.Wallet.Where(w => w.UserId.Equals(userId)).FirstOrDefaultAsync();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");

        wallet.Balance += value;

        Deposit d = new Deposit(value);
        wallet.Transactions.Add(d);

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Withdraw money from current user
    /// </summary>
    /// <param name="value"> Value to withdraw. </param>
    public async Task WithdrawFunds(string userId, double value)
    {
        Wallet? wallet = await context.Wallet.Where(w => w.UserId.Equals(userId)).FirstOrDefaultAsync();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");


        if (wallet.Balance >= value)
        {
            try
            {
                wallet.Balance -= value;

                Withdraw w = new Withdraw(value);

                wallet.Transactions.Add(w);

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro interno!");
            }
        }
        else
        {
            throw new UserBalanceTooLowException("O utilizador não tem saldo suficiente!");
        }


    }


    public async Task<ICollection<TransactionDTO>> GetTransactions(string userId, DateTime start, DateTime end)
    {
        ICollection<Transaction> transactions = await context.Wallet.Where(u => u.Id.Equals(userId))
                                                                    .SelectMany(x => x.Transactions)
                                                                    .Where(x => x.Date > start && x.Date < end) 
                                                                    .ToListAsync();

        ICollection<TransactionDTO> transactionsDTO = new Collection<TransactionDTO>();
        foreach (Transaction transaction in transactions)
        {
            TransactionDTO dto = new TransactionDTO(transaction.Balance, transaction.Date, transaction.GetType().BaseType.Name);
            transactionsDTO.Add(dto);
        }

        return transactionsDTO;
    }

    public async Task AddBetToHistory(string userId, int betId)
    {
        AppUserBetHistory bet = new AppUserBetHistory(userId, betId);
        await context.UserBetHistory.AddAsync(bet);
        await context.SaveChangesAsync();
    }
}
