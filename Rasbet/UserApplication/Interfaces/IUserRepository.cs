using Domain.UserDomain;
using Microsoft.AspNetCore.Mvc;

namespace UserApplication.Interfaces;

public interface IUserRepository
{
	Task<AppUser> RegisterAppUser(string name, string email, string password, string nif, DateTime dob, bool notifications, string language);
	Task<Admin> RegisterAdmin(string name, string email, string password, string language);
	Task<Specialist> RegisterSpecialist(string name, string email, string password, string language);
	Task<User> Login(string email, string password);
	Task Logout();

    Task<User> GetUser(int id);
	Task<AppUser> UpdateAppUser(string email, string name, string language, string coin, bool notifications);
	Task<AppUser> UpdateSensitive(string email, string iban, string nif, DateTime dob, string phoneno);
	Task<Specialist> UpdateSpecialist(string email, string name, string language);
    Task<Admin> UpdateAdmin(string email, string name, string language);
}