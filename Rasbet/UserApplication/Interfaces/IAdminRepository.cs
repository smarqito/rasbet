namespace UserApplication.Interfaces;

public interface IAdminRepository
{

    Task<Admin> RegisterAdmin(string name, string email, string password, string language);

}