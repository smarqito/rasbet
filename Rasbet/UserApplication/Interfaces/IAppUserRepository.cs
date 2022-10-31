namespace UserApplication.Interfaces;

public interface IAppUserRepository
{

    Task<AppUser> RegisterAppUser(string name, string email, string password, string nif, DateTime dob, bool notifications, string language);

}