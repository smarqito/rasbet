using DTO.UserDTO;

namespace UserApplication.Interfaces;

public interface IWalletRepository
{
    Task<WalletDTO> Get(int id);
    Task<Wallet> DepositFunds(int id, double value);
    Task<Wallet> WithdrawFunds(int id, double value);
}