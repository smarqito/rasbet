using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly UserContext context;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AppUserRepository(UserContext context,
                            UserManager<User> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    
    public async Task<AppUser> RegisterAppUser(string name, string email, string password, string nif, DateTime dob , bool notifications, string language)
    {
        AppUser newU = new AppUser(name, email, nif, dob, language, notifications );
        var s = await userManager.CreateAsync(newU, password);
        return newU;
    }

}
