using Domain;
using DTO.BetDTO;
using DTO.UserDTO;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly UserContext context;
    private readonly APIService service;

    public WalletRepository(UserContext context, APIService service)
    {
        this.context = context;
        this.service = service;
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
    public async Task<AppUser> DepositFunds(string id, double value)
    {
        //Transações

        Wallet wallet = context.Wallet.Where(w => w.UserId.Equals(id)).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance += value;

        await context.SaveChangesAsync();

        AppUser user = context.AppUsers.Where(u => u.Id.Equals(id)).First();
        return user;
    }

    /// <summary>
    /// Withdraw money from current user
    /// </summary>
    /// <param name="value"> Value to withdraw. </param>
    public async Task<AppUser> WithdrawFunds(string id, double value)
    {
        
        Wallet wallet = context.Wallet.Where(w => w.UserId.Equals(id)).First();
        if (wallet == null) throw new Exception("O User id indicado não tem uma carteira associada.");
        wallet.Balance -= value;

        await context.SaveChangesAsync();

        AppUser user = context.AppUsers.Where(u => u.Id.Equals(id)).First();
        return user;
    }

    public async Task<AppUser> RegisterBet(string userId, int betId, double value, double odd)
    {
        AppUser user = context.AppUsers.Where(u => u.Id.Equals(userId)).First();

        if (user == null) throw new Exception("Utilizador não encontrado.");

        CreateSelectionDTO sel_dto = 

        CreateBetDTO bet_dto = new CreateBetDTO();
         Amount  Start UserId  selectionDTO { get; set; }

    await service.CreateBetSimple()

        user.BetHistory.Append(betId);

        await context.SaveChangesAsync();

        return user;
    }

}
