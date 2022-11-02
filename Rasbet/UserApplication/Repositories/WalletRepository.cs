using Domain;
using Microsoft.AspNetCore.Identity;
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

}
