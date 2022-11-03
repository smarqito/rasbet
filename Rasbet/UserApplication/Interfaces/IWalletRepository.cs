namespace UserApplication.Interfaces;

public interface IWalletRepository
{
    Task<Wallet> Get(int userId);
    Task<Wallet> DepositFunds(double value);
    Task<Wallet> WithdrawFunds(double value);
}