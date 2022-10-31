using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class SpecialistRepository : ISpecialistRepository
{
	private readonly UserContext context;
	private readonly UserManager<User> userManager;
	private readonly SignInManager<User> signInManager;

	public SpecialistRepository(UserContext context,
						    	UserManager<User> userManager)
	{
		this.context = context;
		this.userManager = userManager;
	}


	public async Task<Specialist> RegisterSpecialist(string name, string email, string password, string language)
	{
		Specialist newU = new Specialist(name, email,language);
		var s = await userManager.CreateAsync(newU, password);
		return newU;
	}

}
