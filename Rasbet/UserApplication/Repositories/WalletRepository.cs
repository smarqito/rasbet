using Domain;
using DTO;
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

    public async Task<AppUser> RegisterBetSimple(CreateSimpleBetDTO dto)
    {
        AppUser user = context.AppUsers.Where(u => u.Id.Equals(dto.UserId)).First();

        if (user == null) throw new Exception("Utilizador não encontrado.");


        BetSimple bet = await service.CreateBetSimple(dto);

        user.BetHistory.Append(bet.Id);

        await context.SaveChangesAsync();

        return user;
    }

    public async Task<AppUser> RegisterBetMult(CreateMultipleBetDTO dto)
    {
        AppUser user = context.AppUsers.Where(u => u.Id.Equals(dto.UserId)).First();

        if (user == null) throw new Exception("Utilizador não encontrado.");


        BetMultiple bet = await service.CreateBetMultiple(dto);

        user.BetHistory.Append(bet.Id);

        await context.SaveChangesAsync();

        return user;
    }
}
