using DTO.UserDTO;
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
    public  async Task<WalletDTO> Get(string id)
    {
        Wallet wallet = context.Wallet.Where(w => w.UserId.Equals(id)).First();

        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");

        WalletDTO dto = new WalletDTO(wallet.UserId, wallet.Balance); 

        return dto;
    }

    /// <summary>
    /// Deposit funds to a user.
    /// </summary>
    /// <param name="value"></param>
    public async Task<Wallet> DepositFunds(string id, double value)
    {
        //Transações

        Wallet wallet = context.Wallet.Where(w => w.UserId.Equals(id)).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance += value;

        await context.SaveChangesAsync();
        return wallet;
    }

    /// <summary>
    /// Withdraw money from current user
    /// </summary>
    /// <param name="value"> Value to withdraw. </param>
    public async Task<Wallet> WithdrawFunds(string id, double value)
    {
       // Withdraw w = new Withdraw(userid,value);
        //await context.Transactions.AddAsync(w);
        
        Wallet wallet = context.Wallet.Where(w => w.UserId.Equals(id)).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance -= value;

        await context.SaveChangesAsync();
        return wallet;
    }
}
