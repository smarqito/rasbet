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

     public async Task<AppUser> RegisterAppUser(string name, string email, string password, string nif, DateTime dob , bool notifications, string language)
    {
        AppUser newU = new AppUser(name, email, nif, dob, language, notifications );
        var s = await userManager.CreateAsync(newU, password);
        return newU;
    }

    public async Task<Admin> RegisterAdmin(string name, string email, string password, string language)
	{
		Admin newU = new Admin(name, email, language);
		var s = await userManager.CreateAsync(newU, password);
		return newU;
	}

    public async Task<Specialist> RegisterSpecialist(string name, string email, string password, string language)
	{
		Specialist newU = new Specialist(name, email,language);
		var s = await userManager.CreateAsync(newU, password);
		return newU;
	}

    public async Task<SignInResult> Login (string email, string password)
    {
        return await signInManager.PasswordSignInAsync(email,password, false, false);
    }

}
