using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext context;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AppUserRepository(UserContext context,
                            UserManager<User> userManager,
                            SignInManager<User> signInManager)
    {
        this.context = context;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    
    public async Task<SignInResult> Login (string email, string password)
    {
        return signInManager.PasswordSignInAsync(email,password, false, false);
    }

}
