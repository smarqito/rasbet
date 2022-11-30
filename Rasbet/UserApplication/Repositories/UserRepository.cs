using Domain.UserDomain;
using DTO.UserDTO;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using UserApplication.Interfaces;
using UserPersistence;

namespace UserApplication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext context;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IJwtGenerator jwtGenerator;

    public UserRepository(UserContext context,
                          UserManager<User> userManager,
                          SignInManager<User> signInManager,
                          RoleManager<IdentityRole> roleManager,
                          IJwtGenerator jwtGenerator)
    {
        this.context = context;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.jwtGenerator = jwtGenerator;
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
    public async Task<AppUser> RegisterAppUser(string name, string email, string password, string passwordRepeated, string nif, DateTime dob, bool notifications, string language)
    {
        if (!password.Equals(passwordRepeated))
            throw new Exception("As 2 passwords são diferentes");
        User user = await userManager.FindByEmailAsync(email);

        var today = DateTime.Today;
        var age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;

        if (age < 18) throw new Exception("Não cumpre a idade mínima permitida.");


        if (user == null)
        {
            AppUser newU = new AppUser(name, email, nif, dob, language, notifications);
            var s = await userManager.CreateAsync(newU, password);

            if (s.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("AppUser"))
                {
                    await roleManager.CreateAsync(new IdentityRole("AppUser"));
                }

                var role = roleManager.FindByNameAsync("AppUser").Result;

                if (role != null)
                {
                    await userManager.AddToRoleAsync(newU, role.Name);

                    return newU;
                }
            }
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

        if (user == null)
        {
            Admin newU = new(name, email, language);
            var s = await userManager.CreateAsync(newU, password);
            if (s.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var role = roleManager.FindByNameAsync("Admin").Result;

                if (role != null)
                {
                    await userManager.AddToRoleAsync(newU, role.Name);

                    return newU;
                }
            }
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
        if (user == null)
        {
            Specialist newU = new Specialist(name, email, language);
            var s = await userManager.CreateAsync(newU, password);
            if (s.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Specialist"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Specialist"));
                }

                var role = roleManager.FindByNameAsync("Specialist").Result;

                if (role != null)
                {
                    await userManager.AddToRoleAsync(newU, role.Name);

                    return newU;
                }
            }
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
    public async Task<UserDTO> Login(string email, string password)
    {
        User user = await userManager.FindByEmailAsync(email);
        if (user == null) throw new Exception("E-mail inexistente.");

        var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        if (result == SignInResult.Success)
        {
            string role = (await userManager.GetRolesAsync(user))[0];
            string token = await jwtGenerator.CreateToken(user);
            UserDTO userDto = new UserDTO(user.Id,user.Name, user.Email, user.Language, token, role);
            return userDto;
        }
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
    public async Task<AppUser> GetAppUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        string role_id = context.UserRoles.Where(u => u.UserId.Equals(id)).First().RoleId;
        IdentityRole role = await roleManager.FindByIdAsync(role_id);
        string role_name = await roleManager.GetRoleNameAsync(role);

        if (user == null) throw new Exception("Utilizador não encontrado.");

        if (role_name == "AppUser") return (AppUser)user;

        throw new Exception("Utilizador não encontrado.");

    }

    /// <summary>
    /// Retrieves an user based on its id.
    /// </summary>
    /// <param name="id"> Id of the user to be retrieved.</param>
    /// <returns>The user, if it exists.</returns>
    /// <exception>The given id doesn't correspond to an user.</exception>
    public async Task<AppUser> GetUserSimple(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user != null) 
            return (AppUser) user;

        throw new Exception("Utilizador não encontrado.");

    }
    public async Task<Specialist> GetSpecialist(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        IdentityUserRole<string> UserRole = await context.UserRoles.Where(u => u.UserId.Equals(id)).FirstOrDefaultAsync();
        if (UserRole == null) throw new Exception("A role nao existe");
        string role_id = UserRole.RoleId;
        IdentityRole role = await roleManager.FindByIdAsync(role_id);
        string role_name = await roleManager.GetRoleNameAsync(role);

        if (user == null) throw new Exception("Utilizador não encontrado.");

        if (role_name == "Specialist") return (Specialist)user;

        throw new Exception("Utilizador não encontrado.");

    }
    public async Task<Admin> GetAdmin(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        IdentityUserRole<string> role = await context.UserRoles.Where(u => u.UserId.Equals(id)).FirstOrDefaultAsync();
        if (role == null) throw new Exception("A role nao existe");
        string role_id = role.RoleId;
        IdentityRole urole = await roleManager.FindByIdAsync(role_id);
        string role_name = await roleManager.GetRoleNameAsync(urole);

        if (user == null) throw new Exception("Utilizador não encontrado.");

        if (role_name == "Admin") return (Admin)user;

        throw new Exception("Utilizador não encontrado.");
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
    public async Task<AppUser> UpdateAppUser(string email, string name, string language, string coin, bool notifications)
    {
        AppUser? user = await context.AppUsers.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (user == null) throw new Exception("E-mail inexistente.");
        if(name != null)
            user.Name = name;
        if (language != null)
            user.Language = language;
        if (coin != null)
            user.Coin = coin;
        user.Notifications = notifications;

        await context.SaveChangesAsync();

        return user;
    }

    public void SendEmail(string to, string subject, string body)
    {
        try
        {
            string from = "rasbet.apostasdesportivas@outlook.com";
            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.EnableSsl = true;
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(from, "Ra$bet2022");
            client.Send(message);
            client.Dispose();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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
    public async Task<AppUser> UpdateAppUserSensitive(string email, string password, string iban, string phoneno)
    {
        AppUser? user = await context.AppUsers.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (user == null) throw new Exception("E-mail inexistente.");

        string code = userManager.GenerateNewAuthenticatorKey();

        UpdateInfo update = new UpdateInfo(email, password, iban, phoneno, code);
        await context.Updates.AddAsync(update);

        await context.SaveChangesAsync();
        string subject = "Confirmação de alterações no perfil";
        string body = $"Olá!\nO seu código de confirmação é {code}.\nBoas apostas.";
        SendEmail(email, subject, body);

        return user;
    }

    /// <summary>
    /// Update sensitive info from an administrator.
    /// </summary>
    /// <param name="email"> Admin's e-mail.</param>
    /// <param name="password"> Admin's password. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Admin> UpdateAdminSensitive(string email, string password)
    {
        Admin? user = await context.Admins.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (user == null) throw new Exception("E-mail inexistente.");

        string code = userManager.GenerateNewAuthenticatorKey();

        UpdateInfo update = new UpdateInfo(email, password, code);
        await context.Updates.AddAsync(update);

        await context.SaveChangesAsync();
        string subject = "Confirmação de alterações no perfil";
        string body = $"Olá!\nO seu código de confirmação é {code}.\nBoas apostas.";
        SendEmail(email, subject, body);

        return user;
    }

    /// <summary>
    /// Update sensitive info from a specialist.
    /// </summary>
    /// <param name="email"> Specialist's e-mail.</param>
    /// <param name="password"> Specialist's password. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Specialist> UpdateSpecialistSensitive(string email, string password)
    {
        Specialist? user = await context.Specialists.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (user == null) throw new Exception("E-mail inexistente.");

        string code = userManager.GenerateNewAuthenticatorKey();

        UpdateInfo update = new UpdateInfo(email, password, code);
        await context.Updates.AddAsync(update);

        await context.SaveChangesAsync();
        string subject = "Confirmação de alterações no perfil";
        string body = $"Olá!\nO seu código de confirmação é {code}.\nBoas apostas.";
        SendEmail(email, subject, body);

        return user;
    }

    /// <summary>
    /// Confirm sensitive info update (previously done) from AppUser.
    /// </summary>
    /// <param name="email"> User's e-mail.</param>
    /// <param name="code"> User's confirmation code. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<AppUser> UpdateAppUserSensitiveConfirm(string email, string code)
    {
        AppUser? user = await context.AppUsers.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        if (user == null) throw new Exception("E-mail inexistente.");

        UpdateInfo? info = await context.Updates.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (info == null) throw new Exception("Utilizador não tem updates.");

        if (code.Equals(info.ConfirmationCode))
        {
            if (info.IBAN != null)
                user.IBAN = info.IBAN;

            if (info.Password != null)
            {
                await userManager.RemovePasswordAsync(user);
                await userManager.AddPasswordAsync(user, info.Password);
            }

            if (info.PhoneNumber != null)
                await userManager.SetPhoneNumberAsync(user, info.PhoneNumber);

            context.Updates.Remove(info);
            await context.SaveChangesAsync();
            return user;
        }
        else
        {
            throw new Exception("Código de confirmação inválido.");
        }

    }

    /// <summary>
    /// Confirm sensitive info update (previously done) from Admin.
    /// </summary>
    /// <param name="email"> Admin's e-mail.</param>
    /// <param name="code"> Admin's confirmation code. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Admin> UpdateAdminSensitiveConfirm(string email, string code)
    {
        Admin? user = await context.Admins.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        if (user == null) throw new Exception("E-mail inexistente.");

        UpdateInfo? info = await context.Updates.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        if (info == null) throw new Exception("Utilizador não tem updates.");

        if (code.Equals(info.ConfirmationCode))
        {
            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, info.Password);
            context.Updates.Remove(info);
            await context.SaveChangesAsync();
            return user;
        }
        else
        {
            throw new Exception("Código de confirmação inválido.");
        }

    }

    /// <summary>
    /// Confirm sensitive info update (previously done) from Specialist.
    /// </summary>
    /// <param name="email"> Specialist's e-mail.</param>
    /// <param name="code"> Specialist's confirmation code. </param>
    /// <returns>The user if the update was successfull. Null otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task<Specialist> UpdateSpecialistSensitiveConfirm(string email, string code)
    {
        Specialist? user = await context.Specialists.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        if (user == null) throw new Exception("E-mail inexistente.");

        UpdateInfo? info = await context.Updates.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        if (info == null) throw new Exception("Utilizador não tem updates.");
        if (code.Equals(info.ConfirmationCode))
        {
            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, info.Password);
            context.Updates.Remove(info);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Código de confirmação inválido.");
        }

        return user;
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
        Specialist? user = await context.Specialists.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (user == null) throw new Exception("E-mail inexistente.");

        if(name != null)
            user.Name = name;
        if (language != null)
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
        Admin? user = await context.Admins.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (user == null) throw new Exception("E-mail inexistente.");

        if(name != null)
            user.Name = name;
        if (language != null)
            user.Language = language;

        await context.SaveChangesAsync();

        return user;
    }

    /// <summary>
    /// Send new password to user.
    /// </summary>
    /// <param name="email"> User's email.</param>
    /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
    /// <exception>The given e-mail doesn't correspond to an user.</exception>
    public async Task ForgotPassword(string email)
    {
        User? user = await context.Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();

        if (user == null) throw new Exception("E-mail inexistente.");

        string new_pw = userManager.GenerateNewAuthenticatorKey();

        await userManager.RemovePasswordAsync(user);
        await userManager.AddPasswordAsync(user, new_pw);

        await context.SaveChangesAsync();


        string subject = "Nova palavra-passe";
        string body = $"Olá!\nA sua nova palavra passe é {new_pw}. Pode alterá-la, se assim desejar, através do nosso site.\nBoas apostas.";

        SendEmail(email, subject, body);
    }
}
