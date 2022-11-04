using DTO.UserDTO;

namespace UserApplication.Interfaces;

public interface IWalletRepository
{
    Task<WalletDTO> Get(string id);
    Task<AppUser> DepositFunds(string id, double value);
    Task<AppUser> WithdrawFunds(string id, double value);
    Task<AppUser> RegisterBet(string userId, int betId, double value, double odd);
}