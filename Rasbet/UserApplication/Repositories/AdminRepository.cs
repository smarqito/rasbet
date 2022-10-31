using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class AdminRepository : IAdminRepository
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


	public async Task<Admin> RegisterAdmin(string name, string email, string password, string language)
	{
		Admin newU = new Admin(name, email, language);
		var s = await userManager.CreateAsync(newU, password);
		return newU;
	}

}