using Domain.UserDomain;
using DTO.GameOddDTO;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace UserApplication.Interfaces;

public interface IUserRepository
{
	Task<AppUser> RegisterAppUser(string name, string email, string password, string passwordRepeated, string nif, DateTime dob, bool notifications, string language);
	Task<Admin> RegisterAdmin(string name, string email, string password, string language);
	Task<Specialist> RegisterSpecialist(string name, string email, string password, string language);
	Task<UserDTO> Login(string email, string password);
	Task Logout();

    Task<AppUser> GetAppUser(string id);
	Task<AppUser> GetUserSimple(string id);
    Task<Specialist> GetSpecialist(string id);
    Task<Admin> GetAdmin(string id);

    Task<AppUser> UpdateAppUser(string email, string name, string language, string coin, bool notifications);
	Task<Specialist> UpdateSpecialist(string email, string name, string language);
    Task<Admin> UpdateAdmin(string email, string name, string language);

	Task<AppUser> UpdateAppUserSensitive(string email, string password, string iban, string phoneno);
	Task<Admin> UpdateAdminSensitive(string email, string password);
	Task<Specialist> UpdateSpecialistSensitive(string email, string password);

	Task<AppUser> UpdateAppUserSensitiveConfirm(string email, string code);
	Task<Admin> UpdateAdminSensitiveConfirm(string email, string code);
	Task<Specialist> UpdateSpecialistSensitiveConfirm(string email, string code);

	Task ForgotPassword(string email);

	Task NotifyChangeGame (ICollection<string> users, string gameState, string homeTeam, string awayTeam, DateTime startTime, ICollection<OddDTO> newOdds);
}