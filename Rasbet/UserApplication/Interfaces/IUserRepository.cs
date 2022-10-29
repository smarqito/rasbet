namespace UserApplication.Interfaces;

public interface IUserRepository
{
    Task<User> RegisterUser(string name, string email, string nif, string phoneNumber, DateTime birthDate, string password);
}