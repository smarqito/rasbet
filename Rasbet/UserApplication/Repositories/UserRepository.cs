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

    
    public async Task<User> Login (string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var password = await userManager.CheckPasswordAsync(user, password);

        if(password)
        {
            var result = await signInManager.SignInAsync(email,password, false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(EmployeeController.Contact), "Employee");
            }
        }
    }

}
