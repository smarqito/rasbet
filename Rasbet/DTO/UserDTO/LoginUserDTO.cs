namespace DTO.LoginUserDTO;

public class LoginUserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginUserDTO(string email, string password) 
    {
        Email = email;
        Password = password;
    }
}