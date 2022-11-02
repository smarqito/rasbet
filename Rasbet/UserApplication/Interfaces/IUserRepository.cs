namespace UserApplication.Interfaces;

public interface IUserRepository
{
	Task<AppUser> RegisterAppUser(string name, string email, string password, string nif, DateTime dob , bool notifications, string language);
	Task<Admin> RegisterAdmin(string name, string email, string password, string language);
	Task<Specialist> RegisterSpecialist(string name, string email, string password, string language);
	Task<SignInResult> Login (string email, string password);
	Task<User> GetUser (int id);
	Task<AppUser> UpdateAppUser (string email, string name, string language, string coin, bool notifications);
}