using Domain;
using Domain.UserDomain;
using DTO.UserDTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly UserContext context;
    private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

    public WalletRepository(UserContext context,
                            Microsoft.AspNetCore.Identity.UserManager<User> usermanager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    /// <summary>
    /// Get user wallet.
    /// </summary>
    /// <param name="userId">Id of the user whose wallet we want to retrieve.</param>
    /// <returns></returns>
    public  async Task<WalletDTO> Get(int id)
    {
        Wallet wallet = context.Wallet.Where(w => w.Id == id).First();

        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");

        WalletDTO dto = new WalletDTO(wallet.Id, wallet.Balance); 

        return dto;
    }

    /// <summary>
    /// Deposit funds to a user.
    /// </summary>
    /// <param name="value"></param>
    public async Task<Wallet> DepositFunds(int id, double value)
    {
        //Transações

        Wallet wallet = context.Wallet.Where(w => w.Id == id).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance += value;

        await context.SaveChangesAsync();
        return wallet;
    }

    /// <summary>
    /// Withdraw money from current user
    /// </summary>
    /// <param name="value"> Value to withdraw. </param>
    public async Task<Wallet> WithdrawFunds(int id, double value)
    {
       // Withdraw w = new Withdraw(userid,value);
        //await context.Transactions.AddAsync(w);
        
        Wallet wallet = context.Wallet.Where(w => w.Id == id).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance -= value;

        await context.SaveChangesAsync();
        return wallet;
    }
}
