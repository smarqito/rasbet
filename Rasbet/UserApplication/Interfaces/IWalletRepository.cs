using DTO.UserDTO;

namespace UserApplication.Interfaces;

public interface IWalletRepository
{
    Task<WalletDTO> Get(string id);
    Task<Wallet> DepositFunds(string id, double value);
    Task<Wallet> WithdrawFunds(string id, double value);
}