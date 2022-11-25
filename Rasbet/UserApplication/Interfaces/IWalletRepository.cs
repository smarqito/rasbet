using DTO;
using DTO.BetDTO;
using DTO.UserDTO;

namespace UserApplication.Interfaces;

public interface IWalletRepository
{
    Task<WalletDTO> Get(string userId);
    Task DepositFunds(string id, double value);
    Task WithdrawFunds(string id, double value);
    Task<AppUser> RegisterBetSimple(CreateSimpleBetDTO dto);
    Task<AppUser> RegisterBetMult(CreateMultipleBetDTO dto);
    Task<ICollection<DTO.UserDTO.TransactionDTO>> GetTransactions(string userId, DateTime start, DateTime end);
}