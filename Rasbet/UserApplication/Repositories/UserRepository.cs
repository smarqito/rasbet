using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext context;
    private readonly UserManager<User> userManager;

    public UserRepository(UserContext context,
                          UserManager<User> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }
    public async Task<User> RegisterUser(string name, string email, string nif, string phoneNumber, DateTime birthDate, string password)
    {
        User newU = new AppUser(name, email, nif, phoneNumber);
        var s = await userManager.CreateAsync(newU, password);
        return newU;
    }
}