using Domain.UserDomain;
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

    /// <summary>
    /// Registers an application user (better).
    /// </summary>
    /// <param name="name"> User's name.</param>
    /// <param name="email"> User's e-mail.</param>
    /// <param name="password"> User's password. </param>
    /// <param name="nif"> User's NIF. </param>
    /// <param name="dob"> User's date of birth. </param>
    /// <param name="notifications"> Indicates whether the user wants to receive notifications or not. </param>
    /// <param name="language"> User's preferred language. </param>
    /// <returns>The user, if the register was successfull.</returns>
    /// <exception>The user is under age or the chosen e-mail is already in use.</exception>
    public async Task<AppUser> RegisterAppUser(string name, string email, string password, string nif, DateTime dob , bool notifications, string language)
    { 
        User user = await userManager.FindByEmailAsync(email);

        var today = DateTime.Today;
        var age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;

        if (age < 18) throw new Exception("Não cumpre a idade mínima permitida.");

        
        if (user == null)
         {
            AppUser newU = new AppUser(name, email, nif, dob, language, notifications);
            var s = await userManager.CreateAsync(newU, password);
            if (s.Succeeded) return newU;
            throw new Exception("Erro ao criar utilizador.");
        }

        throw new Exception("E-mail já está a ser utilizado.");
    }

    /// <summary>
    /// Registers an administrator.
    /// </summary>
    /// <param name="name"> Admin's name.</param>
    /// <param name="email"> Admin's e-mail.</param>
    /// <param name="password"> Admin's password. </param>
    /// <param name="language"> Admin's preferred language. </param>
    /// <returns>The administrator, if the register was successfull.</returns>
    /// <exception>The chosen e-mail is already in use.</exception>
    public async Task<Admin> RegisterAdmin(string name, string email, string password, string language)
	{
        User user = await userManager.FindByEmailAsync(email);

        if (user == null) { 
		    Admin newU = new (name, email, language);
		    var s = await userManager.CreateAsync(newU, password);
		    if (s.Succeeded) return newU;
            throw new Exception("Erro ao criar utilizador.");
        }

        throw new Exception("E-mail já está a ser utilizado.");
	}

    /// <summary>
    /// Registers a specialist.
    /// </summary>
    /// <param name="name"> Specialist's name.</param>
    /// <param name="email"> Specialist's e-mail.</param>
    /// <param name="password"> Specialist's password. </param>
    /// <param name="language"> Specialist's preferred language. </param>
    /// <returns>The specialist, if the register was successfull.</returns>
    /// <exception>The chosen e-mail is already in use.</exception>
    public async Task<Specialist> RegisterSpecialist(string name, string email, string password, string language)
	{
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) {
		    Specialist newU = new Specialist(name, email,language);
		    var s = await userManager.CreateAsync(newU, password);
		    if (s.Succeeded) return newU;
            throw new Exception("Erro ao criar utilizador.");
        }
        throw new Exception("E-mail já está a ser utilizado.");
	}

    /// <summary>
    /// Logs in an user.
    /// </summary>
    /// <param name="email"> Given e-mail.</param>
    /// <param name="password"> Given password. </param>
    /// <returns>The user, if the log in was successfull.</returns>
    /// <exception>The given e-mail doesn't correspond to an user or the password was incorrect.</exception>
    public async Task<User> Login (string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) throw new Exception("E-mail inexistente.");

        var result = await signInManager.PasswordSignInAsync(email,password, false, false);
        if (result == SignInResult.Success)
            return user;
        throw new Exception("Password incorreta.");
    }

    /// <summary>
    /// Logs out an user.
    /// </summary>
    /// <returns></returns>
    public async Task Logout()
    {
        try
        {
            await signInManager.SignOutAsync();
        }
        catch (Exception e)
        {
        }

    }

    /// <summary>
    /// Retrieves an user based on its id.
    /// </summary>
    /// <param name="id"> Id of the user to be retrieved.</param>
    /// <returns>The user, if it exists.</returns>
    /// <exception>The given id doesn't correspond to an user.</exception>
    public async Task<User> GetUser (int id)
    {
        string st_id = id.ToString();
        var user = await userManager.FindByIdAsync(st_id);

        if (user == null) throw new Exception("Utilizador inexistente.");
        return user;
    }

    /// <summary>
    /// Update an application user (better).
    /// </summary>
    /// <param name="email"> User's e-mail.</param>
    /// <param name="name"> User's name.</param>
    /// <param name="language"> User's preferred language. </param>
    /// <param name="coin"> User's preferred coin. </param>
    /// <param name="notifications"> Indicates whether the user wants to receive notifications or not. </param>
    /// <returns>The user if the update was successfull.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<AppUser> UpdateAppUser (string email, string name, string language, string coin, bool notifications)
    {
        AppUser user = context.AppUsers.Where(u => u.Email.Equals(email)).First();

        if (user == null) throw new Exception("E-mail inexistente.");

        user.Name = name;
        user.Language = language;
        user.Coin = coin;
        user.Notifications = notifications;

        await context.SaveChangesAsync();

        return user;
    }

    /// <summary>
    /// Update sensitive info from an application user (better).
    /// </summary>
    /// <param name="email"> User's e-mail.</param>
    /// <param name="password"> User's password. </param>
    /// <param name="iban"> User's iban.</param>
    /// <param name="phoneno">  User's phone number. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<AppUser> UpdateAppUserSensitive(string email, string password, string iban, string phoneno){
        AppUser user = context.AppUsers.Where(u => u.Email.Equals(email)).First();

        if (user == null) throw new Exception("E-mail inexistente.");

        //string code = generate token.....
        //UpdateInfo u = new UpdateInfo(email, password, iban, phoneno, code);
        //context.Updates.Add(u);
        //await context.SaveChangesAsync();
        //send email...

        return null;
    }

    /// <summary>
    /// Update sensitive info from an administrator.
    /// </summary>
    /// <param name="email"> Admin's e-mail.</param>
    /// <param name="password"> Admin's password. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Admin> UpdateAdminSensitive(string email, string password){
        Admin user = context.Admins.Where(u => u.Email.Equals(email)).First();

        if (user == null) throw new Exception("E-mail inexistente.");

        //string code = generate token.....
        //UpdateInfo u = new UpdateInfo(email, password, code);
        //context.Updates.Add(u);
        //await context.SaveChangesAsync();
        //send email...
     

        return null;
    }

    /// <summary>
    /// Update sensitive info from a specialist.
    /// </summary>
    /// <param name="email"> Specialist's e-mail.</param>
    /// <param name="password"> Specilist's password. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Specialist> UpdateSpecialistdSensitive(string email, string password){
        Specialist user = context.Specialists.Where(u => u.Email.Equals(email)).First();

        if (user == null) throw new Exception("E-mail inexistente.");

        //string code = generate token.....
        //UpdateInfo u = new UpdateInfo(email, password, code);
        //await context.SaveChangesAsync();
        //send email...

        return null;
    }

    /// <summary>
    /// Update specialist general info.
    /// </summary>
    /// <param name="email">Specialist's new email.</param>
    /// <param name="name">Specialist's new name..</param>
    /// <param name="language" >Specialist's new language.</param>
    /// <returns>An updated Specialist.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Specialist> UpdateSpecialist(string email, string name, string language)
    {
        Specialist user = context.Specialists.Where(u => u.Email.Equals(email)).First();

        if (user == null) throw new Exception("E-mail inexistente.");

        user.Name = name;
        user.Language = language;

        await context.SaveChangesAsync();

        return user;
    }

    /// <summary>
    /// Update administrator general info.
    /// </summary>
    /// <param name="email">Admin's new email.</param>
    /// <param name="name">Admin's new name..</param>
    /// <param name="language" >Admin's new language.</param>
    /// <returns>An updated Admin.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Admin> UpdateAdmin(string email, string name, string language)
    {
        Admin user = context.Admins.Where(u => u.Email.Equals(email)).First();

        if (user == null) throw new Exception("E-mail inexistente.");

        user.Name = name;
        user.Language = language;

        await context.SaveChangesAsync();

        return user;
    }

}
