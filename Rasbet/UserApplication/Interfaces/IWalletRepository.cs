namespace UserApplication.Interfaces;

public interface IWalletRepository
{
    Task<Wallet> Get(int userId);
    void DepositFunds(double value);
    void WithdrawFunds(double value);
}