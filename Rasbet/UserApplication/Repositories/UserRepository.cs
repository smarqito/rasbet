using Microsoft.AspNetCore.Identity;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext context;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public UserRepository(UserContext context,
                          UserManager<User> userManager,
                          SignInManager<User> signInManager)
    {
        this.context = context;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

     public async Task<AppUser> RegisterAppUser(string name, string email, string password, string nif, DateTime dob , bool notifications, string language)
    {   
        var user = await this.userManager.FindByEmailAsync(email);

        var today = DateTime.Today;
        var age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;

        if (age < 18) throw Exception("Não cumpre a idade mínima permitida.");

        if (user == null) {  
            AppUser newU = new AppUser(name, email, nif, dob, language, notifications );
            var s = await userManager.CreateAsync(newU, password);
            return newU;
        }

        throw new Exception("E-mail já está a ser utilizado.");
    }

    public async Task<Admin> RegisterAdmin(string name, string email, string password, string language)
	{
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) { 
		    Admin newU = new Admin(name, email, language);
		    var s = await userManager.CreateAsync(newU, password);
		    return newU;
        }
        throw new Exception("E-mail já está a ser utilizado.");
	}

    public async Task<Specialist> RegisterSpecialist(string name, string email, string password, string language)
	{
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) {
		    Specialist newU = new Specialist(name, email,language);
		    var s = await userManager.CreateAsync(newU, password);
		    return newU;
        }
        throw new Exception("E-mail já está a ser utilizado.");
	}

    public async Task<SignInResult> Login (string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) throw new Exception("E-mail inexistente.");

        var result = await signInManager.PasswordSignInAsync(email,password, false, false);
        if (result == SignInStatus.Sucess)
            return result;
        throw new Exception("Password incorreta.")
    }

    public async Task<User> GetUser (int id)
    {
        string st_id = id.ToString();
        var user = await userManager.FindByIdAsync(st_id);

        if (user == null) throw Exeption("Utilizador inexistente.");
        return user;
    }

    public async Task<AppUser> UpdateAppUser (string email, string name, string language, string coin, bool notifications)
    {
        AppUser user = await userManager.FindByEmailAsync(email);

        if (user == null) throw Exception("E-mail inexistente.");

        user.Name = name;
        user.Language = language;
        user.Coin = coin;
        user.Notifications = notifications;
        context.SaveChanges();

        return user;
    }

    public async Task<AppUser> UpdateSensitive(string email, string iban, string nif, DateTime dob, string phoneno){
        RegisterAppUser user = await userManager.FindByEmailAsync(email);

        if (user == null) throw Exception("E-mail inexistente.");
        user.IBAN = iban;   
        user.NIF = nif;
        user.PhoneNumber = phoneno;
        user.D
      
    }

}
